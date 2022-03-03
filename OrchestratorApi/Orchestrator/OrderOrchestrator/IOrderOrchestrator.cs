using System;
using OrchestratorApi.Orchestrator.OrderOrchestrator.Commands;
using Rebus.Handlers;
using Rebus.Sagas;
using Shared.Messages.IntegrationEvents;

namespace OrchestratorApi.Orchestrator.OrderOrchestrator
{
    public interface IOrderOrchestrator :
        IHandleMessages<StockUpdateFinishedEvent>,
        IHandleMessages<OrderCreateFinishedEvent>,
        IAmInitiatedBy<OrderProcessStartCommand>
    {
    }
}

