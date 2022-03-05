using System;
using OrderService.BusHandlers.Commands;
using Rebus.Handlers;

namespace OrderService.BusHandlers.CommandHandlers
{
    public interface IOrderServiceCommandHandler : IHandleMessages<StartOrderCreateCommand>
    {
    }
}

