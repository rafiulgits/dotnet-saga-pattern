using OrderService.BusHandlers.CommandHandlers;
using OrderService.BusHandlers.Commands;
using OrderService.Db;
using Rebus.Config;
using Rebus.Messages;
using Rebus.Persistence.InMem;
using Rebus.Routing.TypeBased;
using Shared.Constants;
using Shared.Messages.IntegrationEvents;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddSingleton<OrderDbContext>();
builder.Services.AddRebus(configure => configure
                .Transport(t => t.UseRabbitMq(Settings.RabbitMQEndpoint, Settings.RabbitMQExchangeName))
                .Routing(r =>
                {
                    r.TypeBased()
                        .MapAssemblyOf<Message>(Settings.RabbitMQExchangeName)
                        .MapAssemblyOf<StartOrderCreateCommand>(Settings.RabbitMQExchangeName);
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

builder.Services.AutoRegisterHandlersFromAssemblyOf<OrderServiceCommandHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<OrderDbContext>();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}

app.Services.UseRebus(async bus =>
{
    await bus.Subscribe<StockUpdateCompletedEvent>();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

