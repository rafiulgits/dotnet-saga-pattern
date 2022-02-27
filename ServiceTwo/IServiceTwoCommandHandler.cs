using System;
using Rebus.Handlers;
using ServiceTwo.Commands;

namespace ServiceTwo
{
    public interface IServiceTwoCommandHandler : IHandleMessages<StartServiceTwoCommand>
    {
    }
}

