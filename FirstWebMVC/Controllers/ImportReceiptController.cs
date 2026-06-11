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
    public class ImportReceiptController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ImportReceiptController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ImportReceipt
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ImportReceipts.Include(i => i.Supplier);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ImportReceipt/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importReceipt = await _context.ImportReceipts
                .Include(i => i.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (importReceipt == null)
            {
                return NotFound();
            }

            return View(importReceipt);
        }

        // GET: ImportReceipt/Create
        public IActionResult Create()
        {
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name");
            return View();
        }

        // POST: ImportReceipt/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImportDate,SupplierId")] ImportReceipt importReceipt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(importReceipt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name", importReceipt.SupplierId);
            return View(importReceipt);
        }

        // GET: ImportReceipt/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importReceipt = await _context.ImportReceipts.FindAsync(id);
            if (importReceipt == null)
            {
                return NotFound();
            }
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name", importReceipt.SupplierId);
            return View(importReceipt);
        }

        // POST: ImportReceipt/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImportDate,SupplierId")] ImportReceipt importReceipt)
        {
            if (id != importReceipt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(importReceipt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImportReceiptExists(importReceipt.Id))
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
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name", importReceipt.SupplierId);
            return View(importReceipt);
        }

        // GET: ImportReceipt/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importReceipt = await _context.ImportReceipts
                .Include(i => i.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (importReceipt == null)
            {
                return NotFound();
            }

            return View(importReceipt);
        }

        // POST: ImportReceipt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var importReceipt = await _context.ImportReceipts.FindAsync(id);
            if (importReceipt != null)
            {
                _context.ImportReceipts.Remove(importReceipt);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImportReceiptExists(int id)
        {
            return _context.ImportReceipts.Any(e => e.Id == id);
        }
    }
}
