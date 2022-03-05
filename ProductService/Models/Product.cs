using System;

namespace ProductService.Models
{
    public class Product
    {
        public int Id { get; set; }
        public String? Name { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}

