﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Shop.Domain.Models;
namespace Shop.Database
{
    public class ApplicationDbContext: IdentityDbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options){ }

        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<Stock> Stock { get; set; }

        public DbSet<Order> Orders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OrderProduct>()
                    .HasKey(x => new { x.ProductId, x.OrderId });
        }

    }
}
