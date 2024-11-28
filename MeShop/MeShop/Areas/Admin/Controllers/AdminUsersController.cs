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
    public class AdminUsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

    
        public async Task<IActionResult> Index()
        {
            return View(await _context.Admin_Users.ToListAsync());
        }

     
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Password")] AdminUser adminUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(adminUser);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(adminUser);
            }
            catch (DbUpdateException)
            {
                TempData["error_message"] = "Lỗi";
                return View();
            }
        }

     
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminUser = await _context.Admin_Users.FindAsync(id);
            if (adminUser == null)
            {
                return NotFound();
            }
            return View(adminUser);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Password")] AdminUser adminUser)
        {
            if (id != adminUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    adminUser.Modified_at = DateTime.Now;
                    _context.Update(adminUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminUserExists(adminUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch(DbUpdateException) 
                {
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            return View(adminUser);
        }

 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminUser = await _context.Admin_Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adminUser == null)
            {
                return NotFound();
            }
            return View(adminUser);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adminUser = await _context.Admin_Users.FindAsync(id);
            if (adminUser != null)
            {
                _context.Admin_Users.Remove(adminUser);
            }

            await _context.SaveChangesAsync();
            TempData["success_message"] = "Xóa thành công";
            return RedirectToAction(nameof(Index));
        }

        private bool AdminUserExists(int id)
        {
            return _context.Admin_Users.Any(e => e.Id == id);
        }
    }
}
