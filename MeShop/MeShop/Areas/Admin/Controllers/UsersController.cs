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
using Microsoft.IdentityModel.Tokens;
using System.Drawing.Printing;

namespace MeShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin", AuthenticationSchemes = "AdminScheme")]
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string term = "", string orderBy = "", int currentPage = 1)
        {
            term = String.IsNullOrEmpty(term) ? "" : term.ToLower();

            TempData["term"] = term;
            TempData["orderByName"] = "";
            TempData["orderByEmail"] = "email";

            var applicationDbContext = _context.Users
                .Include(a => a.Addresses)
                .Where(u => u.Name.ToLower().Contains(term) || u.Email.ToLower().Contains(term) || u.Id.ToString().ToLower().Contains(term));

            if (orderBy == "email_desc")
            {
                applicationDbContext = applicationDbContext.OrderByDescending(u => u.Email);
                TempData["orderByEmail"] = "email";
            }
            else if(orderBy == "name_desc")
            {
                applicationDbContext = applicationDbContext.OrderByDescending(u => u.Name);
                TempData["orderByName"] = "";
            }
            else if (orderBy == "email")
            {
                applicationDbContext = applicationDbContext.OrderBy(u => u.Email);
                TempData["orderByEmail"] = "email_desc";
            }
            else
            {
                applicationDbContext = applicationDbContext.OrderBy(u => u.Name);
                TempData["orderByName"] = "name_desc";
            }

            var totalRecords = applicationDbContext.Count();
            int pageSize = 5;
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            applicationDbContext = applicationDbContext.Skip((currentPage - 1) * pageSize).Take(pageSize);

            TempData["totalRecords"] = totalRecords;
            TempData["pageSize"] = pageSize;
            TempData["totalPages"] = totalPages;
            TempData["currentPage"] = currentPage;

            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.Include(u => u.Addresses).Include(u => u.Cart).FirstAsync(u => u.Id == id);
            var cart = user.Cart;
            var cartItems = _context.Cart_Items.Where(i => i.Cart_Id == cart.Id);
            if (user != null)
            {
                _context.Users.Remove(user);  
            }
            if (cart != null)
            {
                _context.Carts.Remove(cart);
            }
            if (cartItems != null)
            {
                _context.Cart_Items.RemoveRange(cartItems);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
