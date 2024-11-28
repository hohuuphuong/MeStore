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
    public class SizesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SizesController(ApplicationDbContext context)
        {
            _context = context;
        }

     
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sizes.ToListAsync());
        }

       
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Size size)
        {
            if (ModelState.IsValid)
            {
                _context.Add(size);
                await _context.SaveChangesAsync();
                TempData["success_message"] = "Thêm thành công";
                return RedirectToAction(nameof(Index));
            }
            return View(size);
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var size = await _context.Sizes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (size == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var size = await _context.Sizes
                    .Include(s => s.Product_Sizes).ThenInclude(ps => ps.Product_Inventory)
                    .FirstOrDefaultAsync(s => s.Id == id);
                if (size != null)
                {
                    if (size.Product_Sizes.Count > 0) 
                    { 
                        _context.Products_Size.RemoveRange(size.Product_Sizes);
                        var Pi = size.Product_Sizes.Select(ps => ps.Product_Inventory).ToList();
                        if (Pi != null)
                        {
                            _context.Products_Inventory.RemoveRange(Pi);
                        }
                    }
                    _context.Sizes.Remove(size);
                }

                await _context.SaveChangesAsync();
                TempData["success_message"] = "Xóa thành công";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["error_message"] = "Lỗi khi xóa";
                return RedirectToAction(nameof(Index));
            }
        }

        private bool SizeExists(int id)
        {
            return _context.Sizes.Any(e => e.Id == id);
        }
    }
}
