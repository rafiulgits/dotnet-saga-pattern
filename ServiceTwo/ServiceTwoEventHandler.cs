using System;
using System.Threading.Tasks;
using Rebus.Bus;
using ServiceTwo.Commands;
using Shared.Messages.IntegrationEvents;

namespace ServiceTwo
{
    public class ServiceTwoEventHandler : IServiceTwoEventHandler
    {
        private readonly IBus bus;

        public ServiceTwoEventHandler(IBus bus)
        {
            this.bus = bus;
        }

        public async Task Handle(ServiceOneCompletedEvent message)
        {
            Console.WriteLine("Service two received event that service one finished its job");
            Console.WriteLine("Service two activating itself");
            await bus.Send(new StartServiceTwoCommand { AggregateId = message.AggregateId } );
        }
    }
}

