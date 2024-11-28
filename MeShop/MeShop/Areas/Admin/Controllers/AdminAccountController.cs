using MeShop.Data;
using MeShop.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MeShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminAccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminAccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated && claimUser.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
                
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminUser adminUser)
        {
            if(ModelState.IsValid)
            {
                var AdminUser = _context.Admin_Users.Where(au => (au.Name == adminUser.Name && au.Password == adminUser.Password)).FirstOrDefault();
                if (AdminUser != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, AdminUser.Name),
                        new Claim(ClaimTypes.Role, "Admin"),
                        new Claim(ClaimTypes.NameIdentifier, AdminUser.Id.ToString())
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, "AdminScheme");

                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddHours(12)
                    };

                    await HttpContext.SignInAsync("AdminScheme", new ClaimsPrincipal(claimsIdentity), properties);
                    TempData["success_message"] = "Đăng nhập thành công";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Sai tên đăng nhập hoặc mật khẩu");
                }
            }
            return View(adminUser);
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = "AdminScheme")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync("AdminScheme");
            return RedirectToAction("login");
        }
    }
}
