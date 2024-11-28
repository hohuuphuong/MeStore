using MeShop.Data;
using MeShop.Models;
using MeShop.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MeShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin", AuthenticationSchemes = "AdminScheme")]
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string term = "", string orderBy = "", int currentPage = 1)
        {
            term = String.IsNullOrEmpty(term) ? "" : term.ToLower();

            TempData["term"] = term;
            TempData["orderByName"] = "";
            TempData["orderByCat"] = "cat";
            TempData["orderBy"] = orderBy;
            var applicationDbContext = _context.Products.Where(p => p.Name.ToLower().Contains(term));

            applicationDbContext = applicationDbContext
                .Include(p => p.Images)
                .Include(p => p.Discount)
                .Include(p => p.Product_Category);

            if (orderBy == "cat_desc")
            {
                applicationDbContext = applicationDbContext.OrderByDescending(p => p.Product_Category.Name);
                TempData["orderByCat"] = "cat";
            }
            else if (orderBy == "cat")
            {
                applicationDbContext = applicationDbContext.OrderBy(p => p.Product_Category.Name);
                TempData["orderByCat"] = "cat_desc";
            }
            else if (orderBy == "name_desc")
            {
                applicationDbContext = applicationDbContext.OrderByDescending(p => p.Name);
                TempData["orderByName"] = "";
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



        public IActionResult Create()
        {
            ViewData["Category_Id"] = new SelectList(_context.Product_Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var product = new Product
                    {
                        Name = model.Name,
                        Price = model.Price,
                        Desc = model.Desc,
                        ProductCategory_Id = model.ProductCategory_Id,
                        Created_At = DateTime.Now,
                        Modified_At = DateTime.Now,
                        Deleted_At = DateTime.Now,
                    };

                    foreach (var formFile in model.ImageFiles)
                    {
                        if (formFile.Length > 0)
                        {
                            // Đặt đường dẫn và tên file
                            var fileName = Path.GetFileName(formFile.FileName);
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                            // Lưu file vào thư mục wwwroot/uploads
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await formFile.CopyToAsync(stream);
                            }

                            // Tạo đối tượng Image và thêm vào Product
                            var image = new Image
                            {
                                Name = fileName,
                                Path = "/uploads/" + fileName,
                                Product_Id = product.Id
                            };
                            product.Images.Add(image);
                        }
                    }
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    TempData["success_message"] = "Thêm mới sản phẩm thành công";
                    return RedirectToAction(nameof(Index));
                }
                ViewData["Category_Id"] = new SelectList(_context.Product_Categories, "Id", "Name");
                return View(model);
            }
            catch (DbUpdateException)
            {
                TempData["error_message"] = "Lỗi";
                ViewData["Category_Id"] = new SelectList(_context.Product_Categories, "Id", "Name");
                return View();
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Images)
                .Include(p => p.Product_Sizes).ThenInclude(s => s.Size)
                .Include(p => p.Product_Sizes).ThenInclude(s => s.Product_Inventory)
                .Include(p => p.Discount)
                .Include(p => p.Product_Category)
                .FirstOrDefaultAsync(p => p.Id == id);
           
            if (product == null)
            {
                return NotFound();
            }
            if (product.Discount != null )
            {
                if (!product.Discount.Active)
                {
                    product.Discount = null;
                    product.Discount_Id = null;
                }      
            }
                
            return View(product);
        }


        [HttpGet]
        public async Task<IActionResult> CreateDetails(int? product_id)
        {
            if (product_id == null) return NotFound();

            var product = await _context.Products.FindAsync(product_id);
            var product_name = product?.Name;
            ViewData["Size_Id"] = new SelectList(_context.Sizes, "Id", "Name");
            ViewData["Product_Id"] = product_id;
            ViewData["Product_Name"] = product_name;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDetails(Product_SizeVM model)
        {
            if (ModelState.IsValid)
            {
                if (_context.Products_Size.Any(ps => ps.Product_Id == model.Product_Id && ps.Size_Id == model.Size_Id))
                {
                    TempData["error_message"] = "Bạn đang cố gắng thêm chi tiết đã tồn tại";
                    return RedirectToAction("Details", new { id = model.Product_Id });
                }
                var product_size = new Product_Size
                {
                    Product_Id = model.Product_Id,
                    Size_Id = model.Size_Id,
                };
                var product_inventory = new Product_Inventory
                {
                    Quantity = model.Quantity,
                    Created_At = DateTime.Now
                };
                product_size.ProductInventory_Id = product_inventory.Id;
                product_size.Product_Inventory = product_inventory;

                _context.Add(product_size);
                await _context.SaveChangesAsync();
                TempData["success_message"] = "Thêm thành công";
                return RedirectToAction("Details", new { id = model.Product_Id });
            }
            ViewData["Size_Id"] = new SelectList(_context.Sizes, "Id", "Name");
            ViewData["Product_Id"] = model.Product_Id;
            ViewData["Product_Name"] = model.Product_Name;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteDetails(int? product_id, int? size_id)
        {
            if (product_id == null || size_id == null)
            {
                return NotFound();
            }

            ViewData["product_id"] = product_id;
            ViewData["size_id"] = size_id;
            var product_size = await _context.Products_Size.FirstOrDefaultAsync(ps => ps.Product_Id == product_id && ps.Size_Id == size_id);
            if (product_size == null)
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost, ActionName("DeleteDetails")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteDetailsConfirmed(int product_id, int size_id)
        {
            var product_size = await _context.Products_Size
                .Where(p => p.Product_Id == product_id && p.Size_Id == size_id)
                .Include(ps => ps.Product_Inventory)
                .FirstOrDefaultAsync();

            var product_inventory = product_size.Product_Inventory;
            if(product_inventory != null)
            {
                _context.Products_Inventory.Remove(product_inventory);
            }
            if (product_size != null)
            {
                _context.Products_Size.Remove(product_size);
                await _context.SaveChangesAsync();
                TempData["success_message"] = "Xóa thành công";
                return RedirectToAction("Details", new { id = product_id });
            }
            TempData["error_message"] = "Xóa thất bại";
            return RedirectToAction("Details", new { id = product_id });
        }



        [HttpGet]
        public IActionResult AddDiscount(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var discounts = _context.Discounts.Where(d => d.Active);
            ViewData["Discount_Id"] = new SelectList(discounts.Select(d =>
                new
                {
                    Id = d.Id,
                    NameCustom = $"{d.Name} ({d.Discount_Percent}%)"
                }),
            "Id", "NameCustom");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDiscount(int id, int Discount_Id)
        {
            var product = await _context.Products.Where(p => p.Id == id).Include(p => p.Discount).FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }
            try
            {
                product.Discount_Id = Discount_Id;
                product.Discount = await _context.Discounts.FindAsync(Discount_Id);
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                ViewData["error_message"] = "";
                return RedirectToAction("Details", new { id = id });
            }
            TempData["success_message"] = "Thêm mã giảm giá thành công";
            return RedirectToAction("Details", new { id = id });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Where(p=> p.Id == id)
                .Include(p => p.Images)
                .Include(p => p.Product_Category)
                .FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }
            var productVM = new ProductVM
            {
                Name = product.Name,
                Price = product.Price,
                ProductCategory_Id = product.ProductCategory_Id.GetValueOrDefault(),
                Images = product.Images,
                Active = product.Active,
                Desc = product.Desc
            };
            
            ViewData["Category_Id"] = new SelectList(_context.Product_Categories, "Id", "Name", productVM.ProductCategory_Id);
            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductVM model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var product = await _context.Products.Where(p=>p.Id == id).Include(p => p.Images).FirstOrDefaultAsync();
                    if(product == null) return NotFound();
                    product.Images.Clear();
                    product.Modified_At = DateTime.Now;
                    product.Name = model.Name;
                    product.Price = model.Price;
                    product.ProductCategory_Id = model.ProductCategory_Id;
                    product.Active = model.Active;
                    product.Desc = model.Desc;


                    for (int i = 0; i < model.existingNameImage.Count; i++) 
                    {
                        if (!string.IsNullOrEmpty(model.existingNameImage[i]) && !string.IsNullOrEmpty(model.existingPathImage[i])) 
                        {
                            var image = new Image
                            {
                                Name = model.existingNameImage[i],
                                Path = model.existingPathImage[i],
                                Product_Id = product.Id
                            };
                            product.Images.Add(image);  
                        }
                    }
                    foreach (var formFile in model.ImageFiles)
                    {
                        if (formFile.Length > 0)
                        {
                            // Đặt đường dẫn và tên file
                            var fileName = Path.GetFileName(formFile.FileName);
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                            // Lưu file vào thư mục wwwroot/uploads
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await formFile.CopyToAsync(stream);
                            }

                            // Tạo đối tượng Image và thêm vào Product
                            var image = new Image
                            {
                                Name = fileName,
                                Path = "/uploads/" + fileName,
                                Product_Id = product.Id
                            };
                            product.Images.Add(image);
                        }
                    }

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    TempData["error_message"] = "Cập nhật thất bại"; 
                    return RedirectToAction(nameof(Index));
                }
                TempData["success_message"] = "Cập nhật thành công";
                return RedirectToAction(nameof(Index));
            }
            TempData["error_message"] = "Cập nhật thất bại";
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View();
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.Where(p => p.Id == id)
                .Include(p => p.Images)
                .Include(p => p.Product_Sizes).ThenInclude(ps => ps.Product_Inventory)
                .Include(p => p.Product_Sizes).ThenInclude(ps => ps.Cart_Items)
                .FirstOrDefaultAsync();
            var cart_item = product.Product_Sizes.SelectMany(ps => ps.Cart_Items);
            var product_inventory = product.Product_Sizes.Select(ps => ps.Product_Inventory).ToList();

            if (product_inventory != null) 
            {
                _context.RemoveRange(product_inventory);
            }
            if (cart_item != null) 
            { 
                _context.RemoveRange(cart_item);
            }

            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                TempData["success_message"] = "Xóa thành công";
                return RedirectToAction(nameof(Index));
            }
            TempData["error_message"] = "Xóa thất bại";
            return RedirectToAction(nameof(Index));
        }
    }
}
