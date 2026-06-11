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
    public class DeviceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeviceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================
        // GET: Device
        // =========================
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Devices
                .Include(d => d.DeviceCategory)
                .Include(d => d.Supplier);

            return View(await applicationDbContext.ToListAsync());
        }

        // =========================
        // GET: Device/Details/5
        // =========================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Devices
                .Include(d => d.DeviceCategory)
                .Include(d => d.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // =========================
        // GET: Device/Create
        // =========================
        public IActionResult Create()
        {
            // FIX DROPDOWN
            ViewData["DeviceCategoryId"] = new SelectList(
                _context.DeviceCategories,
                "Id",
                "Name"
            );

            ViewData["SupplierId"] = new SelectList(
                _context.Suppliers,
                "Id",
                "Name"
            );

            return View();
        }

        // =========================
        // POST: Device/Create
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Name,Price,Quantity,SupplierId,DeviceCategoryId")] Device device)
        {
            if (ModelState.IsValid)
            {
                _context.Add(device);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["DeviceCategoryId"] = new SelectList(
                _context.DeviceCategories,
                "Id",
                "Name",
                device.DeviceCategoryId
            );

            ViewData["SupplierId"] = new SelectList(
                _context.Suppliers,
                "Id",
                "Name",
                device.SupplierId
            );

            return View(device);
        }

        // =========================
        // GET: Device/Edit/5
        // =========================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Devices.FindAsync(id);

            if (device == null)
            {
                return NotFound();
            }

            ViewData["DeviceCategoryId"] = new SelectList(
                _context.DeviceCategories,
                "Id",
                "Name",
                device.DeviceCategoryId
            );

            ViewData["SupplierId"] = new SelectList(
                _context.Suppliers,
                "Id",
                "Name",
                device.SupplierId
            );

            return View(device);
        }

        // =========================
        // POST: Device/Edit/5
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,Name,Price,Quantity,SupplierId,DeviceCategoryId")] Device device)
        {
            if (id != device.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(device);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceExists(device.Id))
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

            ViewData["DeviceCategoryId"] = new SelectList(
                _context.DeviceCategories,
                "Id",
                "Name",
                device.DeviceCategoryId
            );

            ViewData["SupplierId"] = new SelectList(
                _context.Suppliers,
                "Id",
                "Name",
                device.SupplierId
            );

            return View(device);
        }

        // =========================
        // GET: Device/Delete/5
        // =========================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Devices
                .Include(d => d.DeviceCategory)
                .Include(d => d.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // =========================
        // POST: Device/Delete/5
        // =========================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var device = await _context.Devices.FindAsync(id);

            if (device != null)
            {
                _context.Devices.Remove(device);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // CHECK EXISTS
        // =========================
        private bool DeviceExists(int id)
        {
            return _context.Devices.Any(e => e.Id == id);
        }
    }
}