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
    public class ExportReceiptController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExportReceiptController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ExportReceipt
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExportReceipts.ToListAsync());
        }

        // GET: ExportReceipt/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exportReceipt = await _context.ExportReceipts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exportReceipt == null)
            {
                return NotFound();
            }

            return View(exportReceipt);
        }

        // GET: ExportReceipt/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExportReceipt/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExportDate")] ExportReceipt exportReceipt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exportReceipt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exportReceipt);
        }

        // GET: ExportReceipt/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exportReceipt = await _context.ExportReceipts.FindAsync(id);
            if (exportReceipt == null)
            {
                return NotFound();
            }
            return View(exportReceipt);
        }

        // POST: ExportReceipt/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExportDate")] ExportReceipt exportReceipt)
        {
            if (id != exportReceipt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exportReceipt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExportReceiptExists(exportReceipt.Id))
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
            return View(exportReceipt);
        }

        // GET: ExportReceipt/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exportReceipt = await _context.ExportReceipts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exportReceipt == null)
            {
                return NotFound();
            }

            return View(exportReceipt);
        }

        // POST: ExportReceipt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exportReceipt = await _context.ExportReceipts.FindAsync(id);
            if (exportReceipt != null)
            {
                _context.ExportReceipts.Remove(exportReceipt);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExportReceiptExists(int id)
        {
            return _context.ExportReceipts.Any(e => e.Id == id);
        }
    }
}
