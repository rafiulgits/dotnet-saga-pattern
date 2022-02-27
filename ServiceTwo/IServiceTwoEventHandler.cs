using System;
using Rebus.Handlers;
using Shared.Messages.IntegrationEvents;

namespace ServiceTwo
{
    public interface IServiceTwoEventHandler : IHandleMessages<ServiceOneCompletedEvent>
    {
    }
}

