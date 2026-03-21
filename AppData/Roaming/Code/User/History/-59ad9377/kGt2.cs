using CorporateITAssetManagement.Data;
using CorporateITAssetManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace CorporateITAssetManagement.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const string UserSessionKey = "LoggedInUser";
        private const string RoleSessionKey = "UserRole";
        private const string UsernameSessionKey = "Username";

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            // Already logged in check
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString(UserSessionKey)))
                return RedirectToAction("Dashboard", "Admin");
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid input");
                return View();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return View();
            }

            HttpContext.Session.SetString(UserSessionKey, user.Id.ToString());
            HttpContext.Session.SetString(RoleSessionKey, user.Role);
            HttpContext.Session.SetString(UsernameSessionKey, user.Username);

            if (user.Role == "Admin")
                return RedirectToAction("Dashboard", "Admin");
            
            return RedirectToAction("MyEquipments", "Equipment");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        private bool IsLoggedIn()
        {
            return !string.IsNullOrEmpty(HttpContext.Session.GetString(UserSessionKey));
        }

        protected string? GetLoggedInUserId()
        {
            return HttpContext.Session.GetString(UserSessionKey);
        }

        protected string? GetUserRole()
        {
            return HttpContext.Session.GetString(RoleSessionKey);
        }
    }
}
