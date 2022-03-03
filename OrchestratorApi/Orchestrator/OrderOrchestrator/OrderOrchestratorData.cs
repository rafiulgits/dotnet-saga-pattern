using System;
using Rebus.Sagas;

namespace OrchestratorApi.Orchestrator.OrderOrchestrator
{
    public class OrderOrchestratorData : SagaData
    {
        public bool OrderProcessingStarted { get; set; }
        public bool ProductStockUpdated { get; set; }
        public bool OrderCreated { get; set; }
        public bool IsOrderProcessingCompleted =>
            OrderProcessingStarted &&
            ProductStockUpdated &&
            OrderCreated;
    }
}
