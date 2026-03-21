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
                new User { Id = 1, Username = "admin", PasswordHash = BCrypt.Net.BCrypt.HashPassword("1234") }
            };
            dbContext.Users.AddRange(seedData);
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
