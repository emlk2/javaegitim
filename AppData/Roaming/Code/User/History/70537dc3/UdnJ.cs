using CorporateITAssetManagement.Data;
using CorporateITAssetManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CorporateITAssetManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Admin paneli: Tüm personelleri listele
        public async Task<IActionResult> Index()
        {
            if (!IsAdmin())
                return Unauthorized();

            var employees = await _context.Employees
                .Include(e => e.Equipments)
                .ToListAsync();

            return View(employees);
        }

        // Yeni personel ekleme formunu göster
        public IActionResult Create()
        {
            if (!IsAdmin())
                return Unauthorized();

            return View();
        }

        // Yeni personel ekle
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (!IsAdmin())
                return Unauthorized();

            if (ModelState.IsValid)
            {
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // Personel detaylarını göster
        public async Task<IActionResult> Details(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Equipments)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        // Personel düzenleme formunu göster
        public async Task<IActionResult> Edit(int id)
        {
            if (!IsAdmin())
                return Unauthorized();

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        // Personel güncelle
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (!IsAdmin())
                return Unauthorized();

            if (id != employee.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            }

            return View(employee);
        }

        // Personel sil
        public async Task<IActionResult> Delete(int id)
        {
            if (!IsAdmin())
                return Unauthorized();

            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        private bool IsAdmin()
        {
            var role = HttpContext.Session.GetString("UserRole");
            return role == "Admin";
        }
    }
}
