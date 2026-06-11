using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstWebMVC.Data;
using FirstWebMVC.Models;
using FirstWebMVC.ViewModels;
using OfficeOpenXml;

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
                    FacultyName = s.Faculty != null ? s.Faculty.FacultyName : ""
                })
                .ToList();

            return View(data);
        }

        // ===================== CREATE =====================
        public IActionResult Create()
        {
            ViewBag.FacultyList = new SelectList(
                _context.Faculties,
                "FacultyID",
                "FacultyName"
            );

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

            ViewBag.FacultyList = new SelectList(
                _context.Faculties,
                "FacultyID",
                "FacultyName",
                std.FacultyID
            );

            return View(std);
        }

        // ===================== EDIT =====================
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
                return View("NotFound");

            ViewBag.FacultyList = new SelectList(
                _context.Faculties,
                "FacultyID",
                "FacultyName",
                student.FacultyID
            );

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Student std)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.FacultyList = new SelectList(
                    _context.Faculties,
                    "FacultyID",
                    "FacultyName",
                    std.FacultyID
                );

                return View(std);
            }

            var student = await _context.Students.FindAsync(std.Id);

            if (student == null)
                return View("NotFound");

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
                return View("NotFound");

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
                return View("NotFound");

            _context.Students.Remove(student);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // =====================================================
        // ================== BUỔI 10: EXCEL ===================
        // =====================================================

        // 👉 Trang Upload Excel
        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        // 👉 Xử lý Upload Excel
        [HttpPost]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Content("File không hợp lệ");
            }

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                ExcelPackage.License.SetNonCommercialPersonal("Hung");

                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];

                    var rowCount = worksheet.Dimension.Rows;

                    var students = new List<Student>();

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var studentCode = worksheet.Cells[row, 1].Value?.ToString();

                        var fullName = worksheet.Cells[row, 2].Value?.ToString();

                        var facultyText = worksheet.Cells[row, 3].Value?.ToString();

                        int? facultyId = null;

                        // 👉 kiểm tra FacultyID hợp lệ
                        if (!string.IsNullOrEmpty(facultyText))
                        {
                            if (int.TryParse(facultyText, out int tempFacultyId))
                            {
                                bool facultyExists = _context.Faculties
                                    .Any(f => f.FacultyID == tempFacultyId);

                                if (facultyExists)
                                {
                                    facultyId = tempFacultyId;
                                }
                            }
                        }

                        students.Add(new Student
                        {
                            StudentCode = studentCode ?? "",
                            FullName = fullName ?? "",
                            FacultyID = facultyId
                        });
                    }

                    _context.Students.AddRange(students);

                    await _context.SaveChangesAsync();
                }
            }

            return Content("Import Excel thành công!");
        }
    }
}