using ProductService;
using ProductService.BusHandlers.CommandHandlers;
using ProductService.BusHandlers.Commands;
using ProductService.Db;
using Rebus.Config;
using Rebus.Messages;
using Rebus.Persistence.InMem;
using Rebus.Routing.TypeBased;
using Shared.Constants;
using Shared.Messages.IntegrationEvents;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<ProductDbContext>();
builder.Services.AddRebus(configure => configure
                .Transport(t => t.UseRabbitMq(Settings.RabbitMQEndpoint, Settings.RabbitMQExchangeName))
                .Routing(r =>
                {
                    r.TypeBased()
                        .MapAssemblyOf<Message>(Settings.RabbitMQExchangeName)
                        .MapAssemblyOf<StartStockUpdateCommand>(Settings.RabbitMQExchangeName);
                })
                .Sagas(s => s.StoreInMemory())
                .Options(o =>
                {
                    o.SetNumberOfWorkers(1);
                    o.SetMaxParallelism(1);
                    o.SetBusName(Settings.ServiceBusName);
                })
                .Logging(c => c.None())

            );

builder.Services.AutoRegisterHandlersFromAssemblyOf<ProductServiceCommandHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline

using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<ProductDbContext>();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
    new Seed(context);
}

app.Services.UseRebus(async bus =>
{
    await bus.Subscribe<OrderPlaceEvent>();
});

app.MapControllers();

app.Run();
