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
    public class ExportReceiptDetailController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExportReceiptDetailController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================
        // HIỂN THỊ DANH SÁCH
        // =========================
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ExportReceiptDetails
                .Include(e => e.Device)
                .Include(e => e.ExportReceipt);

            return View(await applicationDbContext.ToListAsync());
        }

        // =========================
        // CHI TIẾT
        // =========================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exportReceiptDetail = await _context.ExportReceiptDetails
                .Include(e => e.Device)
                .Include(e => e.ExportReceipt)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (exportReceiptDetail == null)
            {
                return NotFound();
            }

            return View(exportReceiptDetail);
        }

        // =========================
        // GET: CREATE
        // =========================
        public IActionResult Create()
        {
            // Hiển thị tên thiết bị
            ViewData["DeviceId"] = new SelectList(_context.Devices, "Id", "Name");

            // Hiển thị danh sách phiếu xuất
            ViewData["ExportReceiptId"] = new SelectList(_context.ExportReceipts, "Id", "Id");

            return View();
        }

        // =========================
        // POST: CREATE
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExportReceiptId,DeviceId,Quantity,UnitPrice")] ExportReceiptDetail exportReceiptDetail)
        {
            // ===== TỰ ĐỘNG TÍNH THÀNH TIỀN =====
            exportReceiptDetail.TotalPrice =
                exportReceiptDetail.Quantity * exportReceiptDetail.UnitPrice;

            if (ModelState.IsValid)
            {
                _context.Add(exportReceiptDetail);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["DeviceId"] = new SelectList(_context.Devices, "Id", "Name", exportReceiptDetail.DeviceId);

            ViewData["ExportReceiptId"] = new SelectList(_context.ExportReceipts, "Id", "Id", exportReceiptDetail.ExportReceiptId);

            return View(exportReceiptDetail);
        }

        // =========================
        // GET: EDIT
        // =========================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exportReceiptDetail = await _context.ExportReceiptDetails.FindAsync(id);

            if (exportReceiptDetail == null)
            {
                return NotFound();
            }

            ViewData["DeviceId"] = new SelectList(_context.Devices, "Id", "Name", exportReceiptDetail.DeviceId);

            ViewData["ExportReceiptId"] = new SelectList(_context.ExportReceipts, "Id", "Id", exportReceiptDetail.ExportReceiptId);

            return View(exportReceiptDetail);
        }

        // =========================
        // POST: EDIT
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExportReceiptId,DeviceId,Quantity,UnitPrice")] ExportReceiptDetail exportReceiptDetail)
        {
            if (id != exportReceiptDetail.Id)
            {
                return NotFound();
            }

            // ===== TỰ ĐỘNG TÍNH THÀNH TIỀN =====
            exportReceiptDetail.TotalPrice =
                exportReceiptDetail.Quantity * exportReceiptDetail.UnitPrice;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exportReceiptDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExportReceiptDetailExists(exportReceiptDetail.Id))
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

            ViewData["DeviceId"] = new SelectList(_context.Devices, "Id", "Name", exportReceiptDetail.DeviceId);

            ViewData["ExportReceiptId"] = new SelectList(_context.ExportReceipts, "Id", "Id", exportReceiptDetail.ExportReceiptId);

            return View(exportReceiptDetail);
        }

        // =========================
        // DELETE
        // =========================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exportReceiptDetail = await _context.ExportReceiptDetails
                .Include(e => e.Device)
                .Include(e => e.ExportReceipt)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (exportReceiptDetail == null)
            {
                return NotFound();
            }

            return View(exportReceiptDetail);
        }

        // =========================
        // DELETE CONFIRMED
        // =========================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exportReceiptDetail = await _context.ExportReceiptDetails.FindAsync(id);

            if (exportReceiptDetail != null)
            {
                _context.ExportReceiptDetails.Remove(exportReceiptDetail);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // CHECK EXISTS
        // =========================
        private bool ExportReceiptDetailExists(int id)
        {
            return _context.ExportReceiptDetails.Any(e => e.Id == id);
        }
    }
}