﻿using ECommerceWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(

                new Category { Id = 1001, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 1002, Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 1003, Name = "History", DisplayOrder = 3 }

                );
        }
    }
}
