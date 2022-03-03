using System;
using Rebus.Handlers;
using Shared.Messages.IntegrationEvents;

namespace OrderService.BusHandlers.EventHandlers
{
    public interface IOrderServiceEventHandler : IHandleMessages<StockUpdateCompletedEvent>
    {
    }
}

