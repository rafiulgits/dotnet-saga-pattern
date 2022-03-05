using System;
namespace Shared.Messages.IntegrationEvents
{
    public class StockUpdateCompletedEvent : Event
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
    }
}

