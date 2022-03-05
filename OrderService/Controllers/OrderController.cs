using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Db;
using OrderService.Models;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("")]
    public class OrderController : ControllerBase
    {
        private readonly OrderDbContext dbContext;

        public OrderController(OrderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("")]
        public async Task<ActionResult> GetAll()
        {
            var orders = await dbContext.Orders.ToListAsync();
            return Ok(orders);
        }
    }
}

