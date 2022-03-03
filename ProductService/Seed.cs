using System;
using ProductService.Db;
using ProductService.Models;

namespace ProductService
{
    public class Seed
    {
        public Seed(ProductDbContext dbContext)
        {
            dbContext.Products.AddRange(new Product[]
            {
                new Product{ Name = "Mango", Price = 160, StockQuantity = 20 },
                new Product{ Name = "Orange", Price = 200, StockQuantity = 17 },
                new Product{ Name = "Apple", Price = 220 , StockQuantity = 12}
            });
            dbContext.SaveChanges();
        }
    }
}

