using BestHealtStrategies.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static BestHealtStrategies.Models.ValueObjects.ValueObjects;

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
            modelBuilder.Entity<User>()
                .Property(e => e.Intolerances)
                .HasConversion(
                    v => string.Join(',', v.ToString()),
                    v => ParseIntolerances(v.Split(",", StringSplitOptions.RemoveEmptyEntries))
                );
            modelBuilder.Entity<IdentityUser>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            modelBuilder.Entity<IdentityUser>(entity => entity.Property(m => m.NormalizedEmail).HasMaxLength(85));
            modelBuilder.Entity<IdentityUser>(entity => entity.Property(m => m.NormalizedUserName).HasMaxLength(85));

            modelBuilder.Entity<IdentityRole>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            modelBuilder.Entity<IdentityRole>(entity => entity.Property(m => m.NormalizedName).HasMaxLength(85));

            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));

            modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(85));

            modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.Name).HasMaxLength(85));

            modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(85));

            modelBuilder.Entity<Administrator>().HasData(new Administrator(-1, "admin@admin.ba", "admin", "b", "k"));
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

        private List<Intolerance> ParseIntolerances(string[] strings)
        {
            List<Intolerance> intolerances = new List<Intolerance>();
            foreach (string s in strings)
            {
                intolerances.Add((Intolerance)Enum.Parse(typeof(Intolerance), s));
            }
            return intolerances;
        }
    }
}
