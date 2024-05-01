using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tienda_Online.Models;
using Tienda_Online.Services;

namespace Tienda_Online.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LogIn()
        {
            return View();
        }
        [Authorize]
        public IActionResult Cart()
        {
            return View();
        }
        [Authorize]
        public IActionResult Wishlist()
        {
            return View();
        }
        public IActionResult ProductDetails()
        {
            return View();
        }

        [Authorize]
        public IActionResult Account() {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult SingUp()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
