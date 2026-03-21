using CorporateITAssetManagement.Data;
using CorporateITAssetManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CorporateITAssetManagement.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const string UserSessionKey = "LoggedInUser";
        private const string RoleSessionKey = "UserRole";

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => 
                u.Username == username && u.Password == password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return View();
            }

            HttpContext.Session.SetString(UserSessionKey, user.Id.ToString());
            HttpContext.Session.SetString(RoleSessionKey, user.Role);

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
