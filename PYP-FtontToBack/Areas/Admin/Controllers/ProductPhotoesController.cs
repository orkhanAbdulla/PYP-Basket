using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PYP_FtontToBack.DAL;
using PYP_FtontToBack.Models;

namespace PYP_FtontToBack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductPhotoesController : Controller
    {
        private readonly AppDbContext _context;

        public ProductPhotoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ProductPhotoes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ProductPhotos.Include(p => p.Product);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/ProductPhotoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductPhotos == null)
            {
                return NotFound();
            }

            var productPhoto = await _context.ProductPhotos
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productPhoto == null)
            {
                return NotFound();
            }

            return View(productPhoto);
        }

        // GET: Admin/ProductPhotoes/Create
        public IActionResult Create(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductPhoto productPhoto)
        {
            productPhoto.Product = _context.Products.FirstOrDefault(x => x.Id == productPhoto.ProductId);
            productPhoto.Url = "";
            _context.Add(productPhoto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/ProductPhotoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductPhotos == null)
            {
                return NotFound();
            }

            var productPhoto = await _context.ProductPhotos.FindAsync(id);
            if (productPhoto == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", productPhoto.ProductId);
            return View(productPhoto);
        }

        // POST: Admin/ProductPhotoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,Url")] ProductPhoto productPhoto)
        {
            if (id != productPhoto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productPhoto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductPhotoExists(productPhoto.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", productPhoto.ProductId);
            return View(productPhoto);
        }

        // GET: Admin/ProductPhotoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductPhotos == null)
            {
                return NotFound();
            }

            var productPhoto = await _context.ProductPhotos
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productPhoto == null)
            {
                return NotFound();
            }

            return View(productPhoto);
        }

        // POST: Admin/ProductPhotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductPhotos == null)
            {
                return Problem("Entity set 'AppDbContext.ProductPhotos'  is null.");
            }
            var productPhoto = await _context.ProductPhotos.FindAsync(id);
            if (productPhoto != null)
            {
                _context.ProductPhotos.Remove(productPhoto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductPhotoExists(int id)
        {
          return _context.ProductPhotos.Any(e => e.Id == id);
        }
    }
}
