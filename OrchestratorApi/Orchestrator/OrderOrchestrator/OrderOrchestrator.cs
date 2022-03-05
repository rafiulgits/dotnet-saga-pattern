﻿using System;
using OrchestratorApi.Orchestrator.OrderOrchestrator.Commands;
using Rebus.Bus;
using Rebus.Sagas;
using Shared.Messages.IntegrationEvents;

namespace OrchestratorApi.Orchestrator.OrderOrchestrator
{
    public class OrderOrchestrator : Saga<OrderOrchestratorData>, IOrderOrchestrator
    {
        private readonly IBus bus;

        public OrderOrchestrator(IBus bus)
        {
            this.bus = bus;
        }

        public async Task Handle(StockUpdateFinishedEvent message)
        {
            Console.WriteLine("Orchestrator received StockUpdateFinishedEvent");
            var stockUpdateCompletedEvent = new StockUpdateCompletedEvent
            {
                AggregateId = message.AggregateId,
                ProductId = message.ProductId,
                Quantity = message.Quantity,
                Amount = message.Amount
            };
            await bus.Publish(stockUpdateCompletedEvent);
            Data.ProductStockUpdated = true;
            ProcessSagaData();
        }

        public async Task Handle(OrderCreateFinishedEvent message)
        {
            Console.WriteLine("Orchestrator received OrderCreateFinishedEvent");
            Data.OrderCreated = true;
            ProcessSagaData();
        }

        public async Task Handle(OrderProcessStartCommand message)
        {
            Console.WriteLine("Order process start command called");
            var orderPlaceEvent = new OrderPlaceEvent
            {
                AggregateId = message.AggregateId,
                ProductId = message.ProductId,
                Quantity = message.Quantity
            };
            await bus.Publish(orderPlaceEvent);
            Console.WriteLine("Publish event: OrderPlaceEvent");
            Data.OrderProcessingStarted = true;
            ProcessSagaData();
        }

        protected override void CorrelateMessages(ICorrelationConfig<OrderOrchestratorData> config)
        {
            config.Correlate<OrderProcessStartCommand>(m => m.AggregateId, d => d.Id);
            config.Correlate<StockUpdateFinishedEvent>(m => m.AggregateId, d => d.Id);
            config.Correlate<OrderCreateFinishedEvent>(m => m.AggregateId, d => d.Id);
        }

        private void ProcessSagaData()
        {
            if (Data.IsOrderProcessingCompleted)
            {
                Console.WriteLine("Order processing complete!");
                MarkAsComplete();
            }
        }
    }
}

