using System;
using System.Threading.Tasks;
using Rebus.Bus;
using ServiceTwo.Commands;
using Shared.Messages.IntegrationEvents;

namespace ServiceTwo
{
    public class ServiceTwoCommandHandler : IServiceTwoCommandHandler
    {
        private readonly IBus bus;

        public ServiceTwoCommandHandler(IBus bus)
        {
            this.bus = bus;
        }

        public async Task Handle(StartServiceTwoCommand message)
        {
            Console.WriteLine("Service two started");
            await Task.Delay(1000 * 5);
            Console.WriteLine("Service two finished its job");
            await bus.Publish(new ServiceTwoFinishedEvent { AggregateId = message.AggregateId });
        }
    }
}

