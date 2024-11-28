using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeShop.Data;
using MeShop.Models;
using Microsoft.AspNetCore.Authorization;

namespace MeShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin", AuthenticationSchemes = "AdminScheme")]
    [Area("Admin")]
    public class Product_SizeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Product_SizeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Product_Size
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Products_Size.Include(p => p.Product).Include(p => p.Product_Inventory).Include(p => p.Size);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product_Size = await _context.Products_Size
                .Include(p => p.Product)
                .Include(p => p.Product_Inventory)
                .Include(p => p.Size)
                .FirstOrDefaultAsync(m => m.Size_Id == id);
            if (product_Size == null)
            {
                return NotFound();
            }

            return View(product_Size);
        }

        public IActionResult Create()
        {
            ViewData["Product_Id"] = new SelectList(_context.Products, "Id", "Id");
            ViewData["ProductInventory_Id"] = new SelectList(_context.Products_Inventory, "Id", "Id");
            ViewData["Size_Id"] = new SelectList(_context.Sizes, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Product_Id,Size_Id,ProductInventory_Id")] Product_Size product_Size)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product_Size);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Product_Id"] = new SelectList(_context.Products, "Id", "Id", product_Size.Product_Id);
            ViewData["ProductInventory_Id"] = new SelectList(_context.Products_Inventory, "Id", "Id", product_Size.ProductInventory_Id);
            ViewData["Size_Id"] = new SelectList(_context.Sizes, "Id", "Id", product_Size.Size_Id);
            return View(product_Size);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product_Size = await _context.Products_Size
                .Include(p => p.Product)
                .Include(p => p.Product_Inventory)
                .Include(p => p.Size)
                .FirstOrDefaultAsync(m => m.Size_Id == id);
            if (product_Size == null)
            {
                return NotFound();
            }

            return View(product_Size);
        }

        // POST: Admin/Product_Size/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product_Size = await _context.Products_Size.FindAsync(id);
            if (product_Size != null)
            {
                _context.Products_Size.Remove(product_Size);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Product_SizeExists(int id)
        {
            return _context.Products_Size.Any(e => e.Size_Id == id);
        }
    }
}
