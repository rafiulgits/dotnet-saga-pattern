using System;
using Microsoft.AspNetCore.Mvc;
using Orchestrator.Commands;
using Rebus.Bus;

namespace ServiceBus.Controllers
{
    [ApiController]
    [Route("saga")]
    public class SagaController : ControllerBase
    {
        private readonly IBus bus;

        public SagaController(IBus bus)
        {
            this.bus = bus;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var id = Guid.NewGuid();
            await bus.Send(new StartSagaCommand { AggregateId = id });

            return Ok(new { Message = "Saga started", Id = id  });
        }
    }
}

