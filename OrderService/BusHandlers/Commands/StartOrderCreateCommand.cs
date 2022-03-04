using System;
using Shared.Messages;

namespace OrderService.BusHandlers.Commands
{
    public class StartOrderCreateCommand : Command
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
    }
}

