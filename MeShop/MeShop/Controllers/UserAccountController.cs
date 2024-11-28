using MeShop.Data;
using MeShop.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MeShop.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserAccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated && claimUser.IsInRole("User"))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                var User = _context.Users.SingleOrDefault(u => u.Email == user.Email || u.telephone == user.telephone);
                if(User != null)
                {
                    ModelState.AddModelError("", "Số điện thoại hoặc Email đã được sử dụng");
                    return View();
                }
                _context.Users.Add(user);
                _context.SaveChanges();
                TempData["success_message"] = "Đăng ký thành công";
                return RedirectToAction("Login");
            }
            return View();
        }
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated && claimUser.IsInRole("User"))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            if (ModelState.IsValid)
            {
                var User = _context.Users.Where(u => (u.Email == user.Email && u.Password == user.Password)).FirstOrDefault();
                if (User != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, User.Name),
                        new Claim(ClaimTypes.Role, "User"),
                        new Claim(ClaimTypes.Email, User.Email),
                        new Claim(ClaimTypes.NameIdentifier, User.Id.ToString())
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, "UserScheme");

                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddHours(12)
                    };

                    await HttpContext.SignInAsync("UserScheme", new ClaimsPrincipal(claimsIdentity), properties);
                    TempData["success_message"] = "Đăng nhập thành công";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Sai tên đăng nhập hoặc mật khẩu");
                }
            }
            return View(user);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync("UserScheme");
            return RedirectToAction("login");
        }


    }
}
