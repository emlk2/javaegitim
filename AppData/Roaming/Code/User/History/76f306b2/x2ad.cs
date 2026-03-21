using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CorporateITAssetManagement.Models;

namespace CorporateITAssetManagement.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        // Check if user is logged in
        if (HttpContext.Session.GetString("UserId") != null)
        {
            // If logged in, redirect to appropriate dashboard
            var role = HttpContext.Session.GetString("Role");
            if (role == "Admin")
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            else
            {
                return RedirectToAction("MyEquipments", "Equipment");
            }
        }
        else
        {
            // If not logged in, redirect to login
            return RedirectToAction("Login", "Auth");
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
