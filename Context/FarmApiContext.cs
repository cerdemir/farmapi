using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using farmapi.Entities;
using Microsoft.EntityFrameworkCore;

namespace farmapi.Context
{
    public class FarmApiContext : DbContext
    {
        public FarmApiContext(DbContextOptions<FarmApiContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            builder.Entity<Product>().HasQueryFilter(e => e.Deleted == false);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}