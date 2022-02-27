using System;
using System.Threading.Tasks;
using Rebus.Bus;
using ServiceOne.Commands;
using Shared.Messages.IntegrationEvents;

namespace ServiceOne
{
    public class ServiceOneCommandHandler : IServiceOneCommandHandler
    {
        private readonly IBus bus;

        public ServiceOneCommandHandler(IBus bus)
        {
            this.bus = bus;
        }

        public async Task Handle(StartServiceOneCommand message)
        {
            Console.WriteLine("Service one started");
            await Task.Delay(1000 * 5);
            await bus.Publish(new ServiceOneFinishedEvent{ AggregateId = message.AggregateId });
        }
    }
}

