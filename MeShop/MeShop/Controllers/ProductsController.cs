using MeShop.Data;
using MeShop.Models;
using MeShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index(string? Category_Name, string order_by = "", string term = "", int currentPage = 1)
        {
            TempData["order_by"] = order_by;
            TempData["term"] = term;
            List<string> Categories = await _context.Product_Categories.Select(c => c.Name).ToListAsync();
            ViewData["Categories"] = Categories;

            var products = await _context.Products
               .Where(p => p.Active && p.Name.ToLower().Contains(term))
               .Include(p => p.Product_Sizes).ThenInclude(ps => ps.Product_Inventory)
               .Include(p => p.Product_Sizes).ThenInclude(ps => ps.Size)
               .Include(p => p.Product_Category)
               .Include(p => p.Images)
               .Include(p => p.Discount).ToListAsync();
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


            if (String.IsNullOrEmpty(Category_Name) || _context.Product_Categories.FirstOrDefault(c => c.Name == Category_Name) == null)
            {
                ViewData["Category_Name"] = "Tất cả sản phẩm";
            }
            else
            {
                productsVM = productsVM.Where(p => p.ProductCategory_Name == Category_Name).ToList();
                ViewData["Category_Name"] = Category_Name;
            }

            if (order_by == "a")
            {
                _context.Orders?
               .Include(o => o.Orders_Detail).ThenInclude(od => od.Product_Size)
               .ForEachAsync(o =>
               {
                   foreach (var item in o.Orders_Detail.Select(od => new { od.Product_Size, od.Quantity }))
                   {
                       if(productsVM.Find(p => p.Id == item.Product_Size?.Product_Id) != null)
                       {
                           productsVM.Find(p => p.Id == item.Product_Size?.Product_Id).Quantity_Sold += item.Quantity;
                       }
                   }
               });

                productsVM = productsVM.OrderByDescending(p => p.Quantity_Sold).ToList();
            }
            else if (order_by == "b")
                productsVM = productsVM.OrderByDescending(p => p.Rating).ToList();
            else if (order_by == "c")
                productsVM = productsVM.OrderByDescending(p => p.Created_At).ToList();
            else if (order_by == "d")
                productsVM = productsVM.OrderBy(p => p.Price_AfterDiscount).ToList();
            else if (order_by == "e")
                productsVM = productsVM.OrderByDescending(p => p.Price_AfterDiscount).ToList();
            else
                productsVM = productsVM.OrderByDescending(p => p.Created_At).ToList();

            var totalRecords = productsVM.Count();
            int pageSize = 8;
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            productsVM = productsVM.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            TempData["totalRecords"] = totalRecords;
            TempData["pageSize"] = pageSize;
            TempData["totalPages"] = totalPages;
            TempData["currentPage"] = currentPage;

            return View(productsVM);
        }

        [Route("Products/{Collection}")]
        public async Task<IActionResult> Index1(string Collection)
        {
            var products = await _context.Products
                .Where(p => p.Active)
                .Include(p => p.Product_Sizes).ThenInclude(ps => ps.Product_Inventory)
                .Include(p => p.Product_Sizes).ThenInclude(ps => ps.Size)
                .Include(p => p.Product_Category)
                .Include(p => p.Images)
                .Include(p => p.Discount).ToListAsync();

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
            
            
            if(Collection == "popular")
            {
                ViewData["Collection"] = "Phổ biến nhất";
                productsVM = productsVM.OrderByDescending(p => p.Rating).Take(16).ToList();
            }
            else if(Collection == "best_seller")
            {
                _context.Orders?
                .Include(o => o.Orders_Detail).ThenInclude(od => od.Product_Size)
                .ForEachAsync(o =>
                {
                    foreach (var item in o.Orders_Detail.Select(od => new { od.Product_Size, od.Quantity }))
                    {
                        productsVM.Find(p => p.Id == item.Product_Size.Product_Id).Quantity_Sold += item.Quantity;
                    }
                });

                productsVM = productsVM.OrderByDescending(p => p.Quantity_Sold).Take(16).ToList();
                ViewData["Collection"] = "Bán chạy nhất";
            }
            else
            {
                ViewData["Collection"] = "Sản phẩm mới";
                productsVM = productsVM.OrderByDescending(p => p.Created_At).Take(16).ToList();
            }
                

            return View(productsVM);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Where(p => p.Active && p.Id == id)
                .Include(p => p.Product_Sizes).ThenInclude(ps => ps.Product_Inventory)
                .Include(p => p.Product_Sizes).ThenInclude(ps => ps.Size)
                .Include(p => p.Product_Category)
                .Include(p => p.Images)
                .Include(p => p.Discount).FirstOrDefaultAsync();
            if(product == null) 
                return NotFound();

            ProductVM productsVM = new ProductVM()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Desc = product.Desc,
                Created_At = product.Created_At,
                existingPathImage = product.Images.Select(i => i.Path).ToList(),
                Discount_Active = product.Discount?.Active,
                Discount_Percent = product.Discount?.Discount_Percent,
                Rating = product.Rating,
                Product_Sizes = product.Product_Sizes.Where(ps => ps?.Product_Inventory?.Quantity > 0).ToList(),
                ProductCategory_Name = product.Product_Category?.Name
            }; 

            List<Product> related_products = await _context.Products
                .Where(p => p.Active && p.ProductCategory_Id == product.ProductCategory_Id && p.Id != id)
                .Include(p => p.Images)
                .Include(p => p.Discount)
                .Take(6).ToListAsync();

            List<ProductVM> related_productsVM = new List<ProductVM>();
            foreach (var item in related_products)
            {
                related_productsVM.Add(
                    new ProductVM {
                        Id = item.Id,
                        Name = item.Name, 
                        Price = item.Price,
                        Discount_Active= item.Discount?.Active,
                        Discount_Percent= item.Discount?.Discount_Percent,
                        Rating = item.Rating,
                        existingNameImage = item.Images.Select(i => i.Path).ToList(),
                    });
            };

            ViewData["related_products"] = related_productsVM;

            return View(productsVM);
        }
    }
}
