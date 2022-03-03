using System;
using ProductService.BusHandlers.Commands;
using Rebus.Bus;
using Shared.Messages.IntegrationEvents;

namespace ProductService.BusHandlers.CommandHandlers
{
    public class ProductServiceCommandHandler : IProductServiceCommandHandler
    {
        private readonly IBus bus;

        public ProductServiceCommandHandler(IBus bus)
        {
            this.bus = bus;
        }

        public async Task Handle(StartStockUpdateCommand message)
        {
            Console.WriteLine("StockUpdating...");
            //TODO: product stock update database operation
            await Task.Delay(1000 * 5);
            await bus.Publish(new StockUpdateFinishedEvent { AggregateId = message.AggregateId });
            Console.WriteLine("StockUpdateFinishedEvent published");
        }
    }
}

