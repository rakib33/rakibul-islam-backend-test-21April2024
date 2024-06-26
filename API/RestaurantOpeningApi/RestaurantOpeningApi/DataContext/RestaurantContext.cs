﻿using Microsoft.EntityFrameworkCore;
using RestaurantOpeningApi.Models;
using Microsoft.EntityFrameworkCore.Cosmos;

namespace RestaurantOpeningApi.DataContext
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options)
        {
        }
      
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<RestaurantTime> RestaurantTimes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set the table name for the Parent entity
            modelBuilder.Entity<Restaurant>()
                .ToContainer("Restaurant")
                .HasPartitionKey(e => e.Id)
                .HasAlternateKey(x => x.Id); // Specify the table name

            // Set the table name for the Child entity
            modelBuilder.Entity<RestaurantTime>()
                .ToContainer("RestaurantTime")
                .HasPartitionKey(e => e.Id)
                .HasAlternateKey(x => x.Id); // Specify the table name

            modelBuilder.Entity<Restaurant>()
                             .HasMany(p => p.restaurantTimes)
                             .WithOne(c => c.Restaurant)
                             .HasForeignKey(c => c.RestaurantId);

        }
    }
}
