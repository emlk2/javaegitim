using CorporateITAssetManagement.Data;
using CorporateITAssetManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CorporateITAssetManagement.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EquipmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Admin paneli: Tüm cihazları listele
        public async Task<IActionResult> Index()
        {
            if (!IsAdmin())
                return Unauthorized();

            var equipments = await _context.Equipments
                .Include(e => e.AssignedEmployee)
                .ToListAsync();

            return View(equipments);
        }

        // Standart kullanıcı: Kendi zimmetli cihazlarını görmek
        public async Task<IActionResult> MyEquipments()
        {
            var userId = GetLoggedInUserId();
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Auth");

            // Kullanıcının hangi personel olduğunu bulmak gerekli
            // Basit mantık: User ID'si Employee ID ile eşleşir
            var equipments = await _context.Equipments
                .Where(e => e.AssignedEmployeeId == int.Parse(userId))
                .ToListAsync();

            return View(equipments);
        }

        // Yeni cihaz ekleme formunu göster
        public IActionResult Create()
        {
            if (!IsAdmin())
                return Unauthorized();

            return View();
        }

        // Yeni cihaz ekle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Equipment equipment)
        {
            if (!IsAdmin())
                return Unauthorized();

            if (ModelState.IsValid)
            {
                _context.Equipments.Add(equipment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(equipment);
        }

        // Cihaz zimmetleme formunu göster
        public async Task<IActionResult> Assign(int id)
        {
            if (!IsAdmin())
                return Unauthorized();

            var equipment = await _context.Equipments
                .Include(e => e.AssignedEmployee)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (equipment == null)
                return NotFound();

            ViewBag.Employees = new SelectList(
                await _context.Employees.ToListAsync(),
                "Id",
                "FirstName"
            );

            return View(equipment);
        }

        // Cihaz zimmetleme işlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign(int id, int assignedEmployeeId)
        {
            if (!IsAdmin())
                return Unauthorized();

            var equipment = await _context.Equipments.FindAsync(id);
            if (equipment == null)
                return NotFound();

            equipment.AssignedEmployeeId = assignedEmployeeId;
            equipment.Status = "Assigned";

            _context.Update(equipment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // Cihaz zimmetini kaldır (Unassign)
        public async Task<IActionResult> Unassign(int id)
        {
            if (!IsAdmin())
                return Unauthorized();

            var equipment = await _context.Equipments.FindAsync(id);
            if (equipment == null)
                return NotFound();

            equipment.AssignedEmployeeId = null;
            equipment.Status = "Available";

            _context.Update(equipment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // Cihaz detaylarını göster
        public async Task<IActionResult> Details(int id)
        {
            var equipment = await _context.Equipments
                .Include(e => e.AssignedEmployee)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (equipment == null)
                return NotFound();

            return View(equipment);
        }

        // Cihaz sil
        public async Task<IActionResult> Delete(int id)
        {
            if (!IsAdmin())
                return Unauthorized();

            var equipment = await _context.Equipments.FindAsync(id);
            if (equipment != null)
            {
                _context.Equipments.Remove(equipment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        private bool IsAdmin()
        {
            var role = HttpContext.Session.GetString("UserRole");
            return role == "Admin";
        }

        private string? GetLoggedInUserId()
        {
            return HttpContext.Session.GetString("LoggedInUser");
        }
    }
}
