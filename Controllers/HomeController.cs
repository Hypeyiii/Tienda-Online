using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tienda.Controllers;
using Tienda_Online.Models;
using Tienda_Online.Services;

namespace Tienda_Online.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IProductService _productService;

        private readonly ICategoryService _categoryService;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IProductService productService, ICategoryService categoryService) : base(context)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Categories = await _categoryService.GetCategories();

            try
            {
                List<Product> mainproducts = await _productService.GetMainProducts();
                return View(mainproducts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los productos principales");
                return View(new List<Product>());
            }
        }

        public IActionResult ProductDetails(int id)
        {
            var product = _productService.GetProduct(id);
            if(product != null)
            {
                return View(product);
            }
            return View(product);
        }

        public async Task<IActionResult> AddProduct(int id, int quantity, int? categoryID, string? search)
        {
            var cartVM = await AddToCart(id, quantity);

            if(cartVM != null)
            {
                return RedirectToAction("Products", new { categoryID, search });
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> AddProductIndex(int id, int quantity)
        {
            var cartVM = await AddToCart(id, quantity);

            if (cartVM != null)
            {
                return RedirectToAction("Index" , "Cart");
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> AddProductDetails(int id, int quantity)
        {
            var cartVM = await AddToCart(id, quantity);

            if (cartVM != null)
            {
                return RedirectToAction("ProductDetails", new { id });
            }
            else
            {
                return NotFound();
            }
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
