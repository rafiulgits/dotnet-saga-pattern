using System;
using OrderService.BusHandlers.Commands;
using Rebus.Bus;
using Shared.Messages.IntegrationEvents;

namespace OrderService.BusHandlers.EventHandlers
{
    public class OrderServiceEventHandler : IOrderServiceEventHandler
    {
        private readonly IBus bus;

        public OrderServiceEventHandler(IBus bus)
        {
            this.bus = bus;
        }

        public async Task Handle(StockUpdateCompletedEvent message)
        {
            Console.WriteLine("Order service received StockUpdateCompletedEvent");
            Console.WriteLine("StartOrderCreateCommand sending...");
            await bus.Send(new StartOrderCreateCommand { AggregateId = message.AggregateId });
        }
    }
}

