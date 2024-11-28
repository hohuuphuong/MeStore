using MeShop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin", AuthenticationSchemes = "AdminScheme")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetReportByYear(int year = 2024)
        {
            var report = await _context.Orders.Where(o => o.Created_At.Year == year).ToListAsync();

            var monthlyData = new List<decimal?>();

            for (int month = 1; month <= 12; month++)
            {
                var monthlySales = report
                    .Where(o => o.Created_At.Month == month)
                    .Sum(o => o.Total); 

                monthlyData.Add(monthlySales > 0 ? (monthlySales / 1_000_000) : 0);
            }

            var reportData = new
            {
                series = new[]
                {
                    new
                    {
                        name = "Desktops",
                        data = monthlyData
                    }
                }
            };

            return Json(reportData);
        }
    }
}
