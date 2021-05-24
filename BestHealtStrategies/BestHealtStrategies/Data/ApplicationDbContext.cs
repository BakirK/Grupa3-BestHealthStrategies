using BestHealtStrategies.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BestHealtStrategies.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Meal> Meal { get; set; }
        public DbSet<Administrator> Administrator { get; set; }
        public DbSet<DailyMealPlan> DailyMealPlan { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Nutrient> Nutrient { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<ProgressHistory> ProgressHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meal>().ToTable("Meal");
            modelBuilder.Entity<Administrator>().ToTable("Administrator");
            modelBuilder.Entity<DailyMealPlan>().ToTable("DailyMealPlan");
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Nutrient>().ToTable("Nutrient");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Rating>().ToTable("Rating");
            modelBuilder.Entity<ProgressHistory>().ToTable("ProgressHistory");

            base.OnModelCreating(modelBuilder);
        }
    }
}
