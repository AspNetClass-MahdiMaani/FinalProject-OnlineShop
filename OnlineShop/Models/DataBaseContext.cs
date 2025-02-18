using Microsoft.EntityFrameworkCore;
using OnlineShop.Models.DomainModels.PersonAggregates;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace OnlineShop.Models
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Product { get; set; }
    }
}
