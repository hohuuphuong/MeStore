using MeShop.Data;
using MeShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin", AuthenticationSchemes = "AdminScheme")]
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Order> orders = _context.Orders.ToList();
            return View(orders);
        }

        public IActionResult Details(int id)
        {
            var order = _context.Orders.Include(o => o.Orders_Detail).FirstOrDefault();
            if (order == null) return NotFound();

          
            return View(order);
        }
    }
}
