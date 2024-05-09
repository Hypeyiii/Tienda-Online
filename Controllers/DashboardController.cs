using Microsoft.AspNetCore.Mvc;
using Tienda_Online.Services;

namespace Tienda.Controllers
{
    public class DashboardController : BaseController
    {
        public DashboardController(ApplicationDbContext context) : base(context)
        {}
        public IActionResult Index()
        {
            return View();
        }
    }
}