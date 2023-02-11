﻿using Microsoft.EntityFrameworkCore;
using MultiShop.Core.Entities;
using MultiShop.DataAccess.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DataAccess.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfigurations).Assembly);
            modelBuilder.Entity<Product>().HasMany(p => p.Images).WithOne(i => i.Product).HasForeignKey(i => i.ProductId);
        }
    }
}
