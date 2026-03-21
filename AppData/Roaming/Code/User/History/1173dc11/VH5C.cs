using CorporateITAssetManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CorporateITAssetManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Equipment> Equipments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Data: Admin Kullanıcısı
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Password = "1234", // Not: Gerçek projede hash'lenmesi lazım
                    Role = "Admin",
                    CreatedAt = DateTime.UtcNow
                }
            );

            // Seed Data: Örnek Personeller
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    FirstName = "Emel",
                    LastName = "Kılıç",
                    Department = "Engineering",
                    Email = "emel.kilic@company.com",
                    HireDate = new DateTime(2023, 01, 15)
                },
                new Employee
                {
                    Id = 2,
                    FirstName = "Ahmet",
                    LastName = "Yılmaz",
                    Department = "IT Support",
                    Email = "ahmet.yilmaz@company.com",
                    HireDate = new DateTime(2023, 03, 20)
                }
            );

            // Seed Data: Örnek Cihazlar
            modelBuilder.Entity<Equipment>().HasData(
                new Equipment
                {
                    Id = 1,
                    Name = "MacBook Pro 14\"",
                    SerialNumber = "MBPM1A001",
                    Category = "Laptop",
                    Status = "Assigned",
                    PurchaseDate = new DateTime(2023, 01, 10),
                    AssignedEmployeeId = 1
                },
                new Equipment
                {
                    Id = 2,
                    Name = "Dell Monitor 27\"",
                    SerialNumber = "DELLM27001",
                    Category = "Monitor",
                    Status = "Available",
                    PurchaseDate = new DateTime(2023, 02, 05),
                    AssignedEmployeeId = null
                }
            );
        }
    }
}
