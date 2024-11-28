using MeShop.Data;
using MeShop.Helpers;
using MeShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeShop.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        const string CART_KEY = "MYCART";
        public CartVM Cart => HttpContext.Session.Get<CartVM>(CART_KEY) ?? new CartVM();

        public NavbarViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _context.Product_Categories.ToListAsync();

            ViewBag.CartItemCount = Cart.Quantity;
            return View("_NavBar", categories);
        }
    }
}
