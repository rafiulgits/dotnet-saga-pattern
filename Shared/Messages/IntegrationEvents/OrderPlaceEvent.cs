using System;

namespace Shared.Messages.IntegrationEvents
{
    public class OrderPlaceEvent : Event
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

