using System;
using Orchestrator.Commands;
using Rebus.Handlers;
using Rebus.Sagas;
using Shared.Messages.IntegrationEvents;

namespace Orchestrator
{
    public interface IDemoSaga :
        IHandleMessages<ServiceOneFinishedEvent>,
        IHandleMessages<ServiceTwoFinishedEvent>,
        IAmInitiatedBy<StartSagaCommand>
    {
    }
}

