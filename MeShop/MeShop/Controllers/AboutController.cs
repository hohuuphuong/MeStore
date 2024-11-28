using Microsoft.AspNetCore.Mvc;

namespace MeShop.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
