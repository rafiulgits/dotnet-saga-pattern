using System;
using Rebus.Handlers;
using Shared.Messages.IntegrationEvents;

namespace ProductService.BusHandlers.EventHandlers
{
    public interface IProductServiceEventHandler : IHandleMessages<OrderPlaceEvent>
    {
    }
}

