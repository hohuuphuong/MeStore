using Microsoft.AspNetCore.Mvc;

namespace MeShop.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
