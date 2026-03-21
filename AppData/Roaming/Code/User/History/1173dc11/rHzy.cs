using CorporateITAssetManagement.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace CorporateITAssetManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Equipment> Equipments { get; set; } = null!;
    }
}
