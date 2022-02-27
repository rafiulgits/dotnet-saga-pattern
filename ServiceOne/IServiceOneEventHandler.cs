using System;
using Rebus.Handlers;
using Shared.Messages.IntegrationEvents;

namespace ServiceOne
{
    public interface IServiceOneEventHandler : IHandleMessages<SagaStartedEvent>
    {
    }
}

