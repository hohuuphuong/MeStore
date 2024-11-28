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
    public class DiscountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiscountsController(ApplicationDbContext context)
        {
            _context = context;
        }
 
        public async Task<IActionResult> Index()
        {
            return View(await _context.Discounts.ToListAsync());
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Desc,Discount_Percent")] Discount discount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(discount);
                await _context.SaveChangesAsync();
                TempData["success_message"] = "Thêm thành công";
                return RedirectToAction(nameof(Index));
            }
            return View(discount);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discount = await _context.Discounts.FindAsync(id);
            if (discount == null)
            {
                return NotFound();
            }
            return View(discount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Discount discount)
        {
            

            if (ModelState.IsValid)
            {
                try
                {
                    discount.Modified_At = DateTime.Now;
                    _context.Update(discount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscountExists(discount.Id))
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
            return View(discount);
        }

     
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discount = await _context.Discounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discount == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var discount = await _context.Discounts.Include(d => d.Products).FirstOrDefaultAsync(d => d.Id == id);
            try
            {
                if (discount != null)
                {
                    if (discount.Products.Count > 0)
                    {
                        discount.Products.ForEach(p => { p.Discount_Id = null; p.Discount = null; });
                        _context.UpdateRange(discount.Products);
                    }
                    _context.Discounts.Remove(discount);
                    await _context.SaveChangesAsync();
                    TempData["success_message"] = "Xóa thành công";
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            catch (Exception ex) 
            {
                TempData["error_message"] = "Xóa thất bại";
                return RedirectToAction(nameof(Index));
            }
        }

        private bool DiscountExists(int id)
        {
            return _context.Discounts.Any(e => e.Id == id);
        }
    }
}
