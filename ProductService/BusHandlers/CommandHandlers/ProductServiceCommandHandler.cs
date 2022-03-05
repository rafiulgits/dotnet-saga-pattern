using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using ProductService.BusHandlers.Commands;
using ProductService.Db;
using Rebus.Bus;
using Shared.Messages.IntegrationEvents;

namespace ProductService.BusHandlers.CommandHandlers
{
    public class ProductServiceCommandHandler : IProductServiceCommandHandler
    {
        private readonly IBus bus;
        private readonly ProductDbContext dbContext;

        public ProductServiceCommandHandler(IBus bus, ProductDbContext dbContext)
        {
            this.bus = bus;
            this.dbContext = dbContext;
        }

        public async Task Handle(StartStockUpdateCommand message)
        {
            Console.WriteLine("StockUpdating...");

            var productToUpdate = await dbContext.Products.Where(p => p.Id == message.ProductId)
                                                          .FirstAsync();

            productToUpdate.StockQuantity -= message.Quantity;
           
            await dbContext.SaveChangesAsync();

            var stockUpdateFinishedEvent = new StockUpdateFinishedEvent
            {
                AggregateId = message.AggregateId,
                ProductId = message.ProductId,
                Quantity = message.Quantity,
                Amount = message.Quantity * productToUpdate.Price
            };
            await bus.Publish(stockUpdateFinishedEvent);

            Console.WriteLine("StockUpdateFinishedEvent published");
        }
    }
}

