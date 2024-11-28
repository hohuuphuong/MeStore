using MeShop.Data;
using MeShop.Models;
using MeShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MeShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Where(p => p.Active)
                .Include(p => p.Product_Sizes).ThenInclude(ps => ps.Product_Inventory)
                .Include(p => p.Product_Sizes).ThenInclude(ps => ps.Size)
                .Include(p => p.Product_Category)
                .Include(p => p.Images)
                .Include(p => p.Discount).ToListAsync();
   
            ProductVM productVM = new ProductVM();
            List<ProductVM> productsVM = new List<ProductVM>();
            foreach (var item in products)
            {
                productsVM.Add(
                    new ProductVM
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Price = item.Price,
                        Created_At = item.Created_At,
                        existingPathImage = item.Images.Select(i => i.Path).ToList(),
                        Discount_Active = item.Discount?.Active,
                        Discount_Percent = item.Discount?.Discount_Percent,
                        Rating = item.Rating,
                        Product_Sizes = item.Product_Sizes.Where(ps => ps?.Product_Inventory?.Quantity > 0).ToList(),
                        ProductCategory_Name = item.Product_Category?.Name
                    });
            }
            productVM.newest_products = productsVM.OrderByDescending(p => p.Created_At).ToList();
            if (productVM.newest_products.Count > 6)
            {
                productVM.newest_products = productVM.newest_products.Take(6).ToList();
            }

            productVM.popular_products = productsVM.OrderByDescending(p => p.Rating).ToList();
            if (productVM.popular_products.Count > 6)
            {
                productVM.popular_products = productVM.popular_products.Take(6).ToList();
            }

            _context.Orders?
                .Include(o => o.Orders_Detail).ThenInclude(od => od.Product_Size)
                .ForEachAsync(o =>
                {
                    foreach (var item in o.Orders_Detail.Select(od => new {od.Product_Size, od.Quantity}))
                    {
                        productsVM.Find(p => p.Id == item.Product_Size.Product_Id).Quantity_Sold += item.Quantity;
                    }
                });

            productVM.bestseller_products = productsVM.OrderByDescending(p => p.Quantity_Sold).ToList();
            if (productVM.bestseller_products.Count > 6)
            {
                productVM.bestseller_products = productVM.bestseller_products.Take(6).ToList();
            }

            return View(productVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
