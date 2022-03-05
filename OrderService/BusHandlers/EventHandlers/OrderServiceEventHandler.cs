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
            var startOrderCreateCommand = new StartOrderCreateCommand
            {
                AggregateId = message.AggregateId,
                ProductId = message.ProductId,
                Quantity = message.Quantity,
                Amount = message.Amount
            };
            await bus.Send(startOrderCreateCommand);
        }
    }
}

