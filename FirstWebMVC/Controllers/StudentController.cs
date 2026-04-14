using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstWebMVC.Data;
using FirstWebMVC.Models;
using FirstWebMVC.ViewModels;

namespace FirstWebMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ===================== INDEX =====================
        public IActionResult Index()
        {
            var data = _context.Students
                .Include(s => s.Faculty)
                .Select(s => new StudentVM
                {
                    Id = s.Id, 
                    StudentCode = s.StudentCode,
                    FullName = s.FullName,
                    FacultyName = s.Faculty.FacultyName
                })
                .ToList();

            return View(data);
        }

        // ===================== CREATE =====================
        public IActionResult Create()
        {
            ViewBag.FacultyList = new SelectList(_context.Faculties, "FacultyID", "FacultyName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student std)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(std);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.FacultyList = new SelectList(_context.Faculties, "FacultyID", "FacultyName", std.FacultyID);
            return View(std);
        }

        // ===================== EDIT =====================
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return View("NotFound");
            }

            ViewBag.FacultyList = new SelectList(_context.Faculties, "FacultyID", "FacultyName", student.FacultyID);
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Student std)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.FacultyList = new SelectList(_context.Faculties, "FacultyID", "FacultyName", std.FacultyID);
                return View(std);
            }

            
            var student = await _context.Students.FindAsync(std.Id);

            if (student == null)
            {
                return View("NotFound");
            }

            student.StudentCode = std.StudentCode;
            student.FullName = std.FullName;
            student.FacultyID = std.FacultyID;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ===================== DELETE =====================
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students
                .Include(s => s.Faculty)
            
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                return View("NotFound");
            }

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return View("NotFound");
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}