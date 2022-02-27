

using Orchestrator;
using Orchestrator.Commands;
using Rebus.Config;
using Rebus.Messages;
using Rebus.Persistence.InMem;
using Rebus.Routing.TypeBased;
using ServiceOne;
using ServiceOne.Commands;
using ServiceTwo;
using ServiceTwo.Commands;
using Shared.Messages.IntegrationEvents;

var builder = WebApplication.CreateBuilder(args);

const string mqQueue = "queue.exg";

// Add services to the container.
builder.Services.AddRebus(configure => configure
                .Transport(t => t.UseRabbitMq("amqp://localhost:5672", mqQueue))
                .Routing(r =>
                {
                    r.TypeBased()
                        .MapAssemblyOf<Message>(mqQueue)
                        .MapAssemblyOf<StartSagaCommand>(mqQueue)
                        .MapAssemblyOf<StartServiceOneCommand>(mqQueue)
                        .MapAssemblyOf<StartServiceTwoCommand>(mqQueue);
                })
                .Sagas(s => s.StoreInMemory())
                .Options(o =>
                {
                    o.SetNumberOfWorkers(1);
                    o.SetMaxParallelism(1);
                    o.SetBusName("RebusDotnetCore Demo");
                })
                .Logging(c => c.None())
               
            );

builder.Services.AutoRegisterHandlersFromAssemblyOf<DemoSaga>();
builder.Services.AutoRegisterHandlersFromAssemblyOf<ServiceOneCommandHandler>();
builder.Services.AutoRegisterHandlersFromAssemblyOf<ServiceTwoCommandHandler>();

builder.Services.AddControllers();




var app = builder.Build();

// Configure the HTTP request pipeline.

app.Services.UseRebus(async bus =>
{
    await bus.Subscribe<SagaStartedEvent>();
    await bus.Subscribe<ServiceOneFinishedEvent>();
    await bus.Subscribe<ServiceOneCompletedEvent>();
    await bus.Subscribe<ServiceTwoFinishedEvent>();
});

app.UseHttpsRedirection();



app.MapControllers();

app.Run();


