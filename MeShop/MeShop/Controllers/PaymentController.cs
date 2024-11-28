using MeShop.Data;
using MeShop.Helpers;
using MeShop.Models;
using MeShop.Models.ViewModels;
using MeShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MeShop.Controllers
{
    [Authorize(Roles = "User", AuthenticationSchemes = "UserScheme")]
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IVnPayService _vnPayService;

        public PaymentController(ApplicationDbContext context, IVnPayService vnPayService)
        {
            _context = context;
            _vnPayService = vnPayService;
        }
        const string CART_KEY = "MYCART";
        public CartVM Cart => HttpContext.Session.Get<CartVM>(CART_KEY) ?? new CartVM();

        public OrderVM Order => HttpContext.Session.Get<OrderVM>("MYORDER") ?? new OrderVM();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(OrderVM model)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.Set("MYORDER", model);
                CartVM cart = Cart;
                if (cart.Quantity <= 0)
                {
                    TempData["warning_message"] = "Chưa có sản phẩm nào trong giỏ hàng";
                    return RedirectToAction("index","carts");
                }
               
                //Thanh toan VnPay
                if (model.Payment_Method == "VNPAY")
                {
                    var vnPaymentRequestModel = new VnPaymentRequestModel
                    {
                        Amount = (Double)cart.Total,
                        CreatedDate = DateTime.Now,
                        Description = $"{model.Name} {model.Phone}",
                        Name = model.Name,
                        OrderId = (_context.Orders?.Max(o => o.Id) ?? 0) + 1000,
                    };
                    return Redirect(_vnPayService.CreatePaymentUrl(HttpContext, vnPaymentRequestModel));
                }
                //Thanh toan COD
                else
                {
                    var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                    var order = new Order
                    {
                        User_Id = userId,
                        Name = model.Name,
                        Phone = model.Phone,
                        Payment_Method = model.Payment_Method,
                        Email = model.Email,
                        Oder_Note = model.Oder_Note,
                        Address_Detail = model.Address_Detail,
                        Address_City = model.Address_City,
                        Address_District = model.Address_District,
                        Address_Ward = model.Address_Ward,
                        Created_At = DateTime.Now,
                        Total = cart.Total,
                    };
                    _context.Orders.Add(order);
                    _context.SaveChanges();

                    List<Order_Detail> order_detail = new List<Order_Detail>();
                    foreach (var item in cart.Items)
                    {
                        order_detail.Add(new Order_Detail
                        {
                            Order_Id = order.Id,
                            Product_Size_Id = item.ProductSize_Id,
                            Unit_Price = item.Price_AfterDiscount,
                            Quantity = item.quantity,
                            Discount_Percent = item.Discount_Percent ?? 0
                        });
                    }

                    _context.AddRange(order_detail);
                    _context.SaveChanges();

                    HttpContext.Session.Set(CART_KEY, new CartVM());
                    ViewData["order_id"] = order.Id;
                    return View("~/Views/Carts/Checkout_success.cshtml");
                }
            }
            TempData["error_message"] = "Lỗi khi thanh toán";
            return RedirectToAction("index", "carts");
        }

        public IActionResult PaymentCallBack()
        {
            var respon = _vnPayService.PaymentExecute(Request.Query);
            if (respon == null || respon.VnPayResponseCode != "00")
            {
                return View("~/Views/Carts/Checkout_failure.cshtml");
            }

            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            OrderVM orderVM = Order;
            CartVM cart = Cart;
            var order = new Order
            {
                User_Id = userId,
                Name = orderVM.Name,
                Phone = orderVM.Phone,
                Payment_Method = orderVM.Payment_Method,
                Email = orderVM.Email,
                Oder_Note = orderVM.Oder_Note,
                Address_Detail = orderVM.Address_Detail,
                Address_City = orderVM.Address_City,
                Address_District = orderVM.Address_District,
                Address_Ward = orderVM.Address_Ward,
                Created_At = DateTime.Now,
                Total = Cart.Total,
            };
            _context.Orders.Add(order);
            _context.SaveChanges();

            List<Order_Detail> order_detail = new List<Order_Detail>();
            foreach (var item in cart.Items)
            {
                order_detail.Add(new Order_Detail
                {
                    Order_Id = order.Id,
                    Product_Size_Id = item.ProductSize_Id,
                    Unit_Price = item.Price_AfterDiscount,
                    Quantity = item.quantity,
                    Discount_Percent = item.Discount_Percent ?? 0
                });
            }

            _context.AddRange(order_detail);
            _context.SaveChanges();

            HttpContext.Session.Set(CART_KEY, new CartVM());

            ViewData["order_id"] = respon.OrderId;
            return View("~/Views/Carts/Checkout_success.cshtml");
        }
    }
}
