using MeShop.Data;
using MeShop.Models;
using MeShop.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin", AuthenticationSchemes = "AdminScheme")]
    [Area("Admin")]
    public class BlogsController : Controller
    {
       
        private readonly ApplicationDbContext _context;

        public BlogsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Blog> blogs = await _context.Blogs.Include(b => b.Image).ToListAsync();
            List<BlogVM> blogsVM = new List<BlogVM>();
            foreach(var item in blogs)
            {
                blogsVM.Add(new BlogVM
                {
                    Id = item.Id,
                    Title = item.Title,
                    Content = item.Content,
                    Created_At = item.Created_At,
                    ImagePath = item.Image?.Path ?? ""
                });
            }
            return View(blogsVM);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogVM model)
        {
            if (ModelState.IsValid)
            {
                var blog = new Blog
                {
                    Title = model.Title,
                    Content = model.Content,
                    Created_At = DateTime.Now
                };

                if (model.Image != null)
                {
                    var fileName = Path.GetFileName(model.Image.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(stream);
                    }
                    // Lưu file vào thư mục wwwroot/uploads
                    var image = new Image
                    {
                        Name = fileName,
                        Path = "/uploads/" + fileName,
                        Blog = blog
                    };
                    blog.Image = image;
                }
                _context.Blogs.Add(blog);
                await _context.SaveChangesAsync();

                TempData["success_message"] = "Thêm mới Blog thành công";
                return RedirectToAction("Index"); // Redirect to a list or success page
            }
            TempData["error_message"] = "Lỗi khi thêm";
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blog = await _context.Blogs.Include(b => b.Image).FirstOrDefaultAsync(d => d.Id == id);
            try
            {
                if (blog != null)
                {
                    _context.Blogs.Remove(blog);
                    await _context.SaveChangesAsync();
                    TempData["success_message"] = "Xóa thành công";
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                TempData["error_message"] = "Xóa thất bại";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.FindAsync(id);
            var image = await _context.Images.FindAsync(blog?.Image_Id);
            if (blog == null)
            {
                return NotFound();
            }
            var blogVM = new BlogVM
            {
                    Id = blog.Id,
                    Title = blog.Title,
                    Content = blog.Content,
                    ImagePath = image?.Path ?? ""
            };
            return View(blogVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BlogVM model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var blog = await _context.Blogs.FindAsync(id);
                    if(blog == null) return NotFound();
                    blog.Title = model.Title;
                    blog.Content = model.Content;
                    if (model.Image != null)
                    {
                        var fileName = Path.GetFileName(model.Image.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.Image.CopyToAsync(stream);
                        }
                        // Lưu file vào thư mục wwwroot/uploads
                        var image = new Image
                        {
                            Name = fileName,
                            Path = "/uploads/" + fileName,
                        };
                        blog.Image = image;
                    }
                    _context.Update(blog);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                   
                     TempData["error_message"] = "Cập nhật thất bại";
                     return RedirectToAction(nameof(Index));
                    
                }
                TempData["success_message"] = "Cập nhật thành công";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

    }
}
