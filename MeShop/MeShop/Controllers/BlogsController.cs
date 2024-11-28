using MeShop.Data;
using MeShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeShop.Controllers
{
    public class BlogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlogsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<BlogVM> blogsVM = new List<BlogVM>();
            var blogs = await _context.Blogs.Include(b => b.Image).ToListAsync();
            foreach (var item in blogs)
            {
                blogsVM.Add(new BlogVM
                {
                    Id = item.Id,
                    Title = item.Title,
                    //Content = item.Content,
                    Created_At = item.Created_At,
                    ImagePath = item.Image?.Path ?? ""
                });
            }
            return View(blogsVM);
        }
        public async Task<IActionResult> Details(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if(blog == null) return View("NotFound");
            var image = await _context.Images.FindAsync(blog.Image_Id);
            BlogVM blogVM = new BlogVM
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                Created_At = blog.Created_At,
                ImagePath = image?.Path ?? ""
            };
            return View(blogVM);
        }
    }
}
