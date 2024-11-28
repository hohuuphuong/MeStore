using MeShop.Data;
using MeShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MeShop.Controllers
{
    [Authorize(Roles = "User")]
    public class UserAddresses : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserAddresses(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<User_Address> addresses = new List<User_Address>();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                addresses = await _context.User_Addresses.Where(a => a.User_Id.ToString() == userId).ToListAsync();
            }
            return View(addresses);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAddress()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAddress(User_Address model)
        {
            if (ModelState.IsValid)
            {

                var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var Model = new User_Address
                {
                    Address_City = model.Address_City,
                    Address_District = model.Address_District,
                    Address_Ward = model.Address_Ward,
                    Address_Detail = model.Address_Detail,
                    IsDefault = model.IsDefault,
                    User_Id = userId
                };

                _context.User_Addresses.Add(Model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> RemoveAddress(int id)
        {
            var address = await _context.User_Addresses.FindAsync(id);
            if (address == null) return View("Error");
            _context.Remove(address);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
