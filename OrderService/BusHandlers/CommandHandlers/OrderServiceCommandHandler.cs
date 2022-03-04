using System;
using OrderService.BusHandlers.Commands;
using OrderService.Db;
using OrderService.Models;
using Rebus.Bus;
using Shared.Messages.IntegrationEvents;

namespace OrderService.BusHandlers.CommandHandlers
{
    public class OrderServiceCommandHandler : IOrderServiceCommandHandler
    {
        private readonly IBus bus;
        private readonly OrderDbContext dbContext;

        public OrderServiceCommandHandler(IBus bus, OrderDbContext dbContext)
        {
            this.bus = bus;
            this.dbContext = dbContext;
        }

        public async Task Handle(StartOrderCreateCommand message)
        {
            Console.WriteLine("Order creating....");
            var orderToCreate = new Order
            {
                ProductId = message.ProductId,
                Quantity = message.Quantity,
                Amount = message.Amount
            };
            await dbContext.Orders.AddAsync(orderToCreate);
            var count = await dbContext.SaveChangesAsync();
            if(count != 1)
            {
                // save failed
            }
            var orderCreateFinishedEvent = new OrderCreateFinishedEvent
            {
                AggregateId = message.AggregateId,
                ProductId = message.ProductId,
                Quantity = message.Quantity,
                Amount = message.Amount
            };
            await bus.Publish(orderCreateFinishedEvent);
            Console.WriteLine("OrderCreateFinishedEvent publishing");
        }
    }
}

