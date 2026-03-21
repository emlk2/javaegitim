using CorporateITAssetManagement.Data;
using CorporateITAssetManagement.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Cloud ortamı (Render) için dosya tabanlı database kullan
var isCloudEnvironment = Environment.GetEnvironmentVariable("RENDER") != null 
    || Environment.GetEnvironmentVariable("RAILWAY_ENVIRONMENT") != null
    || Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production";

string dbPath = isCloudEnvironment ? "/tmp/corporateit.db" : "corporateit.db";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllersWithViews();

// Configure port for Railway deployment
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

var app = builder.Build();

// Otomatik olarak veritabanı oluştur ve seed data'yı ekle
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<ApplicationDbContext>();
        
        // Veritabanı oluştur
        dbContext.Database.EnsureCreated();
        
        // Seed data'yı ekle (Users tablosunda veri yoksa)
        if (!dbContext.Users.Any())
        {
            var seedData = new List<User>
            {
                new User { Id = 1, Username = "admin", Password = BCrypt.Net.BCrypt.HashPassword("1234"), Role = "Admin", CreatedAt = DateTime.UtcNow }
            };
            dbContext.Users.AddRange(seedData);
            dbContext.SaveChanges();
        }

        // Seed Employees
        if (!dbContext.Employees.Any())
        {
            var employeeData = new List<Employee>
            {
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
            };
            dbContext.Employees.AddRange(employeeData);
            dbContext.SaveChanges();
        }

        // Seed Equipments
        if (!dbContext.Equipments.Any())
        {
            var equipmentData = new List<Equipment>
            {
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
            };
            dbContext.Equipments.AddRange(equipmentData);
            dbContext.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Veritabanı oluşturulurken bir hata meydana geldi.");
        throw;
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Uygulamayı tüm ağ arayüzlerinde (0.0.0.0) ve doğru portta çalıştır
app.Run($"http://0.0.0.0:{port}");
