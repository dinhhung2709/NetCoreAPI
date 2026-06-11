using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstWebMVC.Data;
using FirstWebMVC.Models;

namespace FirstWebMVC.Controllers
{
    public class DeviceCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeviceCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================
        // GET: DeviceCategory
        // SEARCH
        // =========================
        public async Task<IActionResult> Index(string keyword)
        {
            var data = _context.DeviceCategories.AsQueryable();

            // 🔍 Tìm kiếm
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim().ToLower();

                data = data.Where(x =>
                    x.Name != null &&
                    x.Name.ToLower().Contains(keyword)
                );
            }

            return View(await data.ToListAsync());
        }

        // =========================
        // GET: DeviceCategory/Details/5
        // =========================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deviceCategory = await _context.DeviceCategories
                .FirstOrDefaultAsync(m => m.Id == id);

            if (deviceCategory == null)
            {
                return NotFound();
            }

            return View(deviceCategory);
        }

        // =========================
        // GET: DeviceCategory/Create
        // =========================
        public IActionResult Create()
        {
            return View();
        }

        // =========================
        // POST: DeviceCategory/Create
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] DeviceCategory deviceCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deviceCategory);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(deviceCategory);
        }

        // =========================
        // GET: DeviceCategory/Edit/5
        // =========================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deviceCategory = await _context.DeviceCategories.FindAsync(id);

            if (deviceCategory == null)
            {
                return NotFound();
            }

            return View(deviceCategory);
        }

        // =========================
        // POST: DeviceCategory/Edit/5
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] DeviceCategory deviceCategory)
        {
            if (id != deviceCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deviceCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceCategoryExists(deviceCategory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(deviceCategory);
        }

        // =========================
        // GET: DeviceCategory/Delete/5
        // =========================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deviceCategory = await _context.DeviceCategories
                .FirstOrDefaultAsync(m => m.Id == id);

            if (deviceCategory == null)
            {
                return NotFound();
            }

            return View(deviceCategory);
        }

        // =========================
        // POST: DeviceCategory/Delete/5
        // =========================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deviceCategory = await _context.DeviceCategories.FindAsync(id);

            if (deviceCategory != null)
            {
                _context.DeviceCategories.Remove(deviceCategory);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // CHECK EXISTS
        // =========================
        private bool DeviceCategoryExists(int id)
        {
            return _context.DeviceCategories.Any(e => e.Id == id);
        }
    }
}