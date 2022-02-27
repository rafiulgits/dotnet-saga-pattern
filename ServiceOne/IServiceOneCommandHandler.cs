using System;
using Rebus.Handlers;
using ServiceOne.Commands;

namespace ServiceOne
{
    public interface IServiceOneCommandHandler : IHandleMessages<StartServiceOneCommand>
    {
    }
}

