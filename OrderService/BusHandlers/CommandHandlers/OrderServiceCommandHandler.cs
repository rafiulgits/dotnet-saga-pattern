using System;
using OrderService.BusHandlers.Commands;
using Rebus.Bus;
using Shared.Messages.IntegrationEvents;

namespace OrderService.BusHandlers.CommandHandlers
{
    public class OrderServiceCommandHandler : IOrderServiceCommandHandler
    {
        private readonly IBus bus;

        public OrderServiceCommandHandler(IBus bus)
        {
            this.bus = bus;
        }

        public async Task Handle(StartOrderCreateCommand message)
        {
            Console.WriteLine("Order creating....");
            await Task.Delay(1000 * 5);
            //TODO: order entity database entry
            await bus.Publish(new OrderCreateFinishedEvent { AggregateId = message.AggregateId });
            Console.WriteLine("OrderCreateFinishedEvent publishing");
        }
    }
}

