using System;
using Shared.Messages;

namespace OrchestratorApi.Orchestrator.OrderOrchestrator.Commands
{
    public class OrderProcessStartCommand : Command
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

