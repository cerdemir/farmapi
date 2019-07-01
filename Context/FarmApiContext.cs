using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using farmapi.Entities;
using Microsoft.EntityFrameworkCore;

namespace farmapi.Context {
    public class FarmApiContext : DbContext {
        public FarmApiContext (DbContextOptions<FarmApiContext> options) : base (options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}