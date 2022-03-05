using System;
using ProductService.BusHandlers.Commands;
using Rebus.Bus;
using Shared.Messages.IntegrationEvents;

namespace ProductService.BusHandlers.EventHandlers
{
    public class ProductServiceEventHandler : IProductServiceEventHandler
    {
        private readonly IBus bus;

        public ProductServiceEventHandler(IBus bus)
        {
            this.bus = bus;
        }

        public async Task Handle(OrderPlaceEvent message)
        {
            Console.WriteLine("Product service received that 'OrderPlaceEvent' fired");
            Console.WriteLine("StartStockUpdateCommand sending");
            var startStockUpdateCommand = new StartStockUpdateCommand
            {
                AggregateId = message.AggregateId,
                ProductId = message.ProductId,
                Quantity = message.Quantity
            };
            await bus.Send(startStockUpdateCommand);
        }
    }
}

