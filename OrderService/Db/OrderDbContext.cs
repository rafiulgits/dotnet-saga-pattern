using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OrderService.Models;

namespace OrderService.Db
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=OrdceerService.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
            base.OnConfiguring(optionsBuilder);
        }
    }
}

