using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ProductService.Models;

namespace ProductService.Db
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=ProductService.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
            base.OnConfiguring(optionsBuilder);
        }

    }
}

