using System;
using System.Threading.Tasks;
using Rebus.Bus;
using ServiceOne.Commands;
using Shared.Messages.IntegrationEvents;

namespace ServiceOne
{
    public class ServiceOneEventHandler : IServiceOneEventHandler
    {
        private readonly IBus bus;

        public ServiceOneEventHandler(IBus bus)
        {
            this.bus = bus;
        }

        public async Task Handle(SagaStartedEvent message)
        {
            Console.WriteLine("Service one received event that 'Saga' has been started");
            Console.WriteLine("Service one commanding itself to execute operations");
            await bus.Send(new StartServiceOneCommand { AggregateId = message.AggregateId });
        }
    }
}

