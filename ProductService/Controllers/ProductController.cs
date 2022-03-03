using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductService.Db;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("")]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext dbContext;

        public ProductController(ProductDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("")]
        public async Task<ActionResult> GetAll()
        {
            var products = await dbContext.Products.ToListAsync();
            return Ok(products);
        }


    }
}

