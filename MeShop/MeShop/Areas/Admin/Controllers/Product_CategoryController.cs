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
    public class Product_CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Product_CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

    
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product_Categories.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product_Category = await _context.Product_Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product_Category == null)
            {
                return NotFound();
            }

            return View(product_Category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Desc")] Product_Category product_Category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product_Category);
                await _context.SaveChangesAsync();
                TempData["success_message"] = "Thêm mới danh mục thành công";
                return RedirectToAction(nameof(Index));
            }
            TempData["error_message"] = "Thêm mới thất bại";
            return View(product_Category);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product_Category = await _context.Product_Categories.FindAsync(id);
            if (product_Category == null)
            {
                return NotFound();
            }
            return View(product_Category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Desc")] Product_Category product_Category)
        {
            if (id != product_Category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product_Category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Product_CategoryExists(product_Category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        TempData["error_message"] = "Cập nhật thất bại";
                        return RedirectToAction(nameof(Index));
                    }
                }
                TempData["success_message"] = "Cập nhật thành công";
                return RedirectToAction(nameof(Index));
            }
            return View(product_Category);
        }

      
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product_Category = await _context.Product_Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product_Category == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product_Category = await _context.Product_Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
            var products = product_Category.Products;

            if (products != null) 
            {
                products.ForEach(p => { p.ProductCategory_Id = null; p.Product_Category = null; });
                _context.Products.UpdateRange(products);
                
            }
            if (product_Category != null)
            {
                _context.Product_Categories.Remove(product_Category);
            }

            await _context.SaveChangesAsync();
            TempData["success_message"] = "Xóa thành công";
            return RedirectToAction(nameof(Index));
        }

        private bool Product_CategoryExists(int id)
        {
            return _context.Product_Categories.Any(e => e.Id == id);
        }
    }
}
