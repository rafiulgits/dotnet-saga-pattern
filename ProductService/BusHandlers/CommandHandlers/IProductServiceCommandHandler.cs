using System;
using ProductService.BusHandlers.Commands;
using Rebus.Handlers;

namespace ProductService.BusHandlers.CommandHandlers
{
    public interface IProductServiceCommandHandler : IHandleMessages<StartStockUpdateCommand>
    {
    }
}

