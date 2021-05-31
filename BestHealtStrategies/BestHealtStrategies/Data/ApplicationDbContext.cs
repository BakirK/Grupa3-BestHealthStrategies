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
    public class ApplicationDbContext : IdentityDbContext<User>
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
            // custom parser from list of enums to csv string and vice versa
            modelBuilder.Entity<User>()
                .Property(e => e.Intolerances)
                .HasConversion(
                    v => ConvertIntolerancesToCsvString(v),
                    v => ParseIntolerancesFromCSV(v.Split(",", StringSplitOptions.RemoveEmptyEntries))
                );
            
            const string ADMIN_ID = "ff06b223-45b2-4556-95b6-2e5ed16d81c9";
            const string USER_ID = "26d10b61-c3e2-4863-bad6-6d0f4e131529";
            const string EMPLOYEE_ID = "4922c26e-a27f-4faa-a685-593e945800fd";

            // create default roles
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "1",
                Name = "administrator",
                NormalizedName = "ADMINISTRATOR"
            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "2",
                Name = "employee",
                NormalizedName = "EMPLOYEE"
            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "3",
                Name = "user",
                NormalizedName = "USER"
            });

            //create default admin account
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = ADMIN_ID,
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                //pass: adminAdmin1!
                PasswordHash = "AQAAAAEAACcQAAAAEKPE+ptiverXhuV8Eau9ggI/OHy8JJ8E3vs4yJWObKA7hBWRQqU/XPJ3JAWu1cGPMw==",
                SecurityStamp = "6XEUQE2NPXXCX5ZP2G5UE464PONO64WM",
                ConcurrencyStamp = "08337cc0-be1d-41a8-a4b5-7a4ad1213406",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                Name = "Admin",
                Surname = "Admin",
                Role = Role.ADMIN,
                Age = 21,
                Height = 166,
                Weight = 85,
                Gender = Gender.FEMALE,
                Activity = ActivityLevel.NOEXERCISE,
                Benefit = Benefit.FATLOSS,
                Diet = Diet.GLUTENFREE,
                Intolerances = new List<Intolerance>(){
                    Intolerance.GRAIN
                }
            });
            // assigen admin role to admin user account
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "1",
                UserId = ADMIN_ID
            });

            //create default employee account
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = EMPLOYEE_ID,
                UserName = "emp@emp.com",
                NormalizedUserName = "EMP@EMP.COM",
                Email = "emp@emp.com",
                NormalizedEmail = "EMP@EMP.COM",
                EmailConfirmed = true,
                //pass: empEMP1!
                PasswordHash = "AQAAAAEAACcQAAAAEEer+FUD/moTD9x26JCHAxg/u6I10TfQE7gkalp42Nk4nzcRCWe0I6h2apqBmDpK8w==",
                SecurityStamp = "LMPASNLSKERMILNMSV2LKW3PM6FV3PXI",
                ConcurrencyStamp = "29510329-fc0f-4d8f-b173-23a3985d5819",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                Name = "Employee",
                Surname = "Employee",
                Role = Role.EMPLOYEE,
                Age = 21,
                Height = 187,
                Weight = 90,
                Gender = Gender.MALE,
                Activity = ActivityLevel.HARD,
                Benefit = Benefit.MUSCLEGAIN,
                Diet = Diet.PALEO,
                Intolerances = new List<Intolerance>(){
                }
            });

            // assign employee role to employee user account
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "2",
                UserId = EMPLOYEE_ID
            });

            // create default user
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = USER_ID,
                UserName = "user@user.com",
                NormalizedUserName = "USER@USER.COM",
                Email = "user@user.com",
                NormalizedEmail = "USER@USER.COM",
                EmailConfirmed = true,
                //pass: userUSER1!
                PasswordHash = "AQAAAAEAACcQAAAAEPJn1nOxdkfLf8c3VeqvN93gXrXLkZ32Lz126ZAqSo7FLC4/cqxfIelp1EudM0QYKA==",
                SecurityStamp = "PU6ZQF7NGAVGPNRR5ULZVOZ4NDSS4E6J",
                ConcurrencyStamp = "a137d51a-e6c9-4c8b-aea2-d9aefca51d4b",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                Name = "Bakir",
                Surname = "Karovic",
                Role = Role.USER,
                Age = 21,
                Height = 185,
                Weight = 74,
                Gender = Gender.MALE,
                Activity = ActivityLevel.LIGHT,
                Benefit = Benefit.ENERGY,
                Diet = Diet.KETOGENIC,
                Intolerances = new List<Intolerance>(){
                    Intolerance.DAIRY,
                    Intolerance.SOY
                }
            });

            // assign user role to user account
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "3",
                UserId = USER_ID
            });

            // map classes to tables
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

        private string ConvertIntolerancesToCsvString(List<Intolerance> intolerances)
        {
            StringBuilder sb = new StringBuilder("");
            foreach (Intolerance intolerance in intolerances)
            {
                sb.Append(intolerance.ToString());
                sb.Append(",");
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
        private List<Intolerance> ParseIntolerancesFromCSV(string[] strings)
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
