
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
    public class ImportReceiptDetailController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ImportReceiptDetailController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================================================
        // BƯỚC 9: HIỂN THỊ DỮ LIỆU LIÊN KẾT
        // Include Device và ImportReceipt
        // =========================================================

        // GET: ImportReceiptDetail
        public async Task<IActionResult> Index()
        {
            var applicationDbContext =
                _context.ImportReceiptDetails
                .Include(i => i.Device)
                .Include(i => i.ImportReceipt);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ImportReceiptDetail/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // =========================================================
            // BƯỚC 9: Include dữ liệu liên kết
            // =========================================================

            var importReceiptDetail = await _context.ImportReceiptDetails
                .Include(i => i.Device)
                .Include(i => i.ImportReceipt)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (importReceiptDetail == null)
            {
                return NotFound();
            }

            return View(importReceiptDetail);
        }

        // GET: ImportReceiptDetail/Create
        public IActionResult Create()
        {
            // =========================================================
            // BƯỚC 6: Dropdown phiếu nhập
            // =========================================================

            ViewData["ImportReceiptId"] =
                new SelectList(_context.ImportReceipts,
                "Id", "Id");

            // =========================================================
            // BƯỚC 6: Dropdown thiết bị
            // Hiển thị Name thay vì Id
            // =========================================================

            ViewData["DeviceId"] =
                new SelectList(_context.Devices,
                "Id", "Name");

            return View();
        }

        // POST: ImportReceiptDetail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,ImportReceiptId,DeviceId,Quantity,UnitPrice,TotalPrice")]
            ImportReceiptDetail importReceiptDetail)
        {
            if (ModelState.IsValid)
            {
                // =========================================================
                // BƯỚC 7: TỰ TÍNH THÀNH TIỀN
                // =========================================================

                importReceiptDetail.TotalPrice =
                    importReceiptDetail.Quantity *
                    importReceiptDetail.UnitPrice;

                // =========================================================
                // BƯỚC 8: CẬP NHẬT TỒN KHO
                // =========================================================

                var device = await _context.Devices
                    .FindAsync(importReceiptDetail.DeviceId);

                if (device != null)
                {
                    device.Quantity += importReceiptDetail.Quantity;
                }

                _context.Add(importReceiptDetail);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // =========================================================
            // BƯỚC 6: Load lại dropdown nếu lỗi validation
            // =========================================================

            ViewData["ImportReceiptId"] =
                new SelectList(_context.ImportReceipts,
                "Id", "Id",
                importReceiptDetail.ImportReceiptId);

            ViewData["DeviceId"] =
                new SelectList(_context.Devices,
                "Id", "Name",
                importReceiptDetail.DeviceId);

            return View(importReceiptDetail);
        }

        // GET: ImportReceiptDetail/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importReceiptDetail =
                await _context.ImportReceiptDetails.FindAsync(id);

            if (importReceiptDetail == null)
            {
                return NotFound();
            }

            // =========================================================
            // BƯỚC 6: Dropdown Edit
            // =========================================================

            ViewData["ImportReceiptId"] =
                new SelectList(_context.ImportReceipts,
                "Id", "Id",
                importReceiptDetail.ImportReceiptId);

            ViewData["DeviceId"] =
                new SelectList(_context.Devices,
                "Id", "Name",
                importReceiptDetail.DeviceId);

            return View(importReceiptDetail);
        }

        // POST: ImportReceiptDetail/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,ImportReceiptId,DeviceId,Quantity,UnitPrice,TotalPrice")]
            ImportReceiptDetail importReceiptDetail)
        {
            if (id != importReceiptDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // =========================================================
                    // BƯỚC 7: Tính lại thành tiền khi Edit
                    // =========================================================

                    importReceiptDetail.TotalPrice =
                        importReceiptDetail.Quantity *
                        importReceiptDetail.UnitPrice;

                    _context.Update(importReceiptDetail);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImportReceiptDetailExists(importReceiptDetail.Id))
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

            // =========================================================
            // BƯỚC 6: Load dropdown nếu Edit lỗi
            // =========================================================

            ViewData["ImportReceiptId"] =
                new SelectList(_context.ImportReceipts,
                "Id", "Id",
                importReceiptDetail.ImportReceiptId);

            ViewData["DeviceId"] =
                new SelectList(_context.Devices,
                "Id", "Name",
                importReceiptDetail.DeviceId);

            return View(importReceiptDetail);
        }

        // GET: ImportReceiptDetail/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // =========================================================
            // BƯỚC 9: Include dữ liệu liên kết
            // =========================================================

            var importReceiptDetail = await _context.ImportReceiptDetails
                .Include(i => i.Device)
                .Include(i => i.ImportReceipt)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (importReceiptDetail == null)
            {
                return NotFound();
            }

            return View(importReceiptDetail);
        }

        // POST: ImportReceiptDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var importReceiptDetail =
                await _context.ImportReceiptDetails.FindAsync(id);

            if (importReceiptDetail != null)
            {
                _context.ImportReceiptDetails.Remove(importReceiptDetail);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ImportReceiptDetailExists(int id)
        {
            return _context.ImportReceiptDetails
                .Any(e => e.Id == id);
        }
    }
}

