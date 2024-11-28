using MeShop.Data;
using MeShop.Helpers;
using MeShop.Models;
using MeShop.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MeShop.Controllers
{
    public class CartsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartsController(ApplicationDbContext context)
        {
            _context = context;
        }

        const string CART_KEY = "MYCART";
        public CartVM Cart => HttpContext.Session.Get<CartVM>(CART_KEY) ?? new CartVM();
        public OrderVM Order => HttpContext.Session.Get<OrderVM>("MYORDER") ?? new OrderVM();

        public async Task<IActionResult> Index()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated && claimUser.IsInRole("User"))
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId != null)
                {
                    ViewData["AddressList"] = await _context.User_Addresses.Where(a => a.User_Id.ToString() == userId).ToListAsync();
                }
            }
            ViewData["Order"] = Order;
            return View(Cart);
        }

        public async Task<IActionResult> AddToCart(int? cart_id, int product_size_id, int quantity = 1)
        {
            CartVM cart = Cart;
            var item = cart.Items.SingleOrDefault(c => c.ProductSize_Id == product_size_id);

            if (item == null)
            {
                var Item = await _context.Products_Size
                   .Include(p => p.Product).ThenInclude(p => p.Images)
                   .Include(p => p.Product).ThenInclude(p => p.Discount)
                   .Include(p => p.Size)
                   .SingleOrDefaultAsync(p => p.Id == product_size_id);
                if (Item == null) return NotFound();
                item = new CartItemVM
                {
                    ProductSize_Id = Item.Id,
                    ProductName = Item.Product.Name,
                    SizeName = Item.Size.Name,
                    quantity = quantity,
                    PathImage = Item.Product?.Images?.FirstOrDefault()?.Path,
                    Discount_Active = Item.Product?.Discount?.Active,
                    Discount_Percent = Item.Product?.Discount?.Discount_Percent,
                    Price = Item.Product.Price,
                };
                cart.Items.Add(item);
            }
            else
            {
                item.quantity += quantity;
            }
            cart.Quantity += quantity;
            cart.Total = cart.Items.Sum(i => i.Price_AfterDiscount * i.quantity);
            HttpContext.Session.Set(CART_KEY, cart);

            var cartItemCount = cart.Quantity;

            return Json(new { success = true, message = "Đã thêm sản phẩm vào giỏ hàng", cartItemCount = cartItemCount });
        }

        public IActionResult RemoveItem(int? cart_id, int product_size_id)
        {
            CartVM cart = Cart;
            var item = cart.Items.SingleOrDefault(c => c.ProductSize_Id == product_size_id);
            if (item == null) return NotFound();
            cart.Quantity -= item.quantity;
            cart.Total -= item.quantity * item.Price_AfterDiscount;    
            cart.Items.Remove(item);
            HttpContext.Session.Set(CART_KEY, cart);
            return RedirectToAction("Index");
        }

    }
}
