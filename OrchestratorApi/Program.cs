using OrchestratorApi.Orchestrator.OrderOrchestrator;
using OrchestratorApi.Orchestrator.OrderOrchestrator.Commands;
using Rebus.Config;
using Rebus.Messages;
using Rebus.Persistence.InMem;
using Rebus.Routing.TypeBased;
using Shared.Constants;
using Shared.Messages.IntegrationEvents;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddRebus(configure => configure
                .Transport(t => t.UseRabbitMq(Settings.RabbitMQEndpoint, Settings.RabbitMQExchangeName))
                .Routing(r =>
                {
                    r.TypeBased()
                        .MapAssemblyOf<Message>(Settings.RabbitMQExchangeName)
                        .MapAssemblyOf<OrderProcessStartCommand>(Settings.RabbitMQExchangeName);
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

builder.Services.AutoRegisterHandlersFromAssemblyOf<OrderOrchestrator>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.


app.Services.UseRebus(async bus =>
{
    await bus.Subscribe<StockUpdateFinishedEvent>();
    await bus.Subscribe<OrderCreateFinishedEvent>();
});

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

