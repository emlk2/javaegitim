using CorporateITAssetManagement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CorporateITAssetManagement.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Dashboard()
        {
            if (!IsAdmin())
                return Unauthorized();

            var totalEmployees = await _context.Employees.CountAsync();
            var totalEquipments = await _context.Equipments.CountAsync();
            var assignedEquipments = await _context.Equipments
                .Where(e => e.Status == "Assigned")
                .CountAsync();
            var availableEquipments = await _context.Equipments
                .Where(e => e.Status == "Available")
                .CountAsync();

            ViewBag.TotalEmployees = totalEmployees;
            ViewBag.TotalEquipments = totalEquipments;
            ViewBag.AssignedEquipments = assignedEquipments;
            ViewBag.AvailableEquipments = availableEquipments;

            return View();
        }

        private bool IsAdmin()
        {
            var role = HttpContext.Session.GetString("UserRole");
            return role == "Admin";
        }
    }
}
