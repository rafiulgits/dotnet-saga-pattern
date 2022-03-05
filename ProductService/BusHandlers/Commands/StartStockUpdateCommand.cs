using System;
using Shared.Messages;

namespace ProductService.BusHandlers.Commands
{
    public class StartStockUpdateCommand : Command
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

