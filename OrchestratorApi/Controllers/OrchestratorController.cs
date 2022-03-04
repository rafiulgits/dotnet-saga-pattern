using System;
using Microsoft.AspNetCore.Mvc;
using OrchestratorApi.Orchestrator.OrderOrchestrator.Commands;
using Rebus.Bus;

namespace OrchestratorApi.Controllers
{
    [ApiController]
    [Route("")]
    public class OrchestratorController : ControllerBase
    {
        private readonly IBus bus;

        public OrchestratorController(IBus bus)
        {
            this.bus = bus;
        }

        [HttpGet("")]
        public async Task<ActionResult> StartOrderProcess()
        {
            var id = Guid.NewGuid();
            await bus.Send(new OrderProcessStartCommand { AggregateId = id, ProductId = 2, Quantity = 5 });

            return Ok(new { Message = "Order process started", Id = id, ProductId = 2, Quantity = 5 });
        }
    }
}

