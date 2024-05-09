using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tienda.Controllers;
using Tienda_Online.Services;
using Tienda_Online.Models;

namespace Tienda_Online
{
    public class CartController : BaseController
    {
        public CartController(ApplicationDbContext context) : base(context)
        {
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var cartVM = await GetCartVM();

            foreach (var item in cartVM.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductID);
                if (product != null)
                {
                    item.Product = product;

                    if (item.Quantity > product.Stock)
                    {
                        item.Quantity = product.Stock;
                    }
                }
                else
                {
                    item.Quantity = 0;
                }
            }
            var userID = User.Identity?.IsAuthenticated == true ? int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)) : 0;

            var locations = User.Identity?.IsAuthenticated == true ? _context.Location.Where(l => l.UserID == userID).ToList() : new List<Location>();

            var goToShipping = new GoToShippingVM
            {
                Cart = cartVM,
                Locations = locations,
            };

            return View(goToShipping);
        }

        [HttpPost]
        public async Task<IActionResult> ReloadQuantity(int id, int quantity)
        {
            var cartVM = await GetCartVM();
            var cartItem = cartVM.Items.FirstOrDefault(i => i.ProductID == id);

            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                var product = await _context.Products.FindAsync(id);
                if (product != null && product.Stock > 0)
                {
                    cartItem.Quantity = Math.Min(cartItem.Quantity, product.Stock);
                }
                await SaveCartVM(cartVM);
            }
            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItem(int id)
        {
            var cartVM = await GetCartVM();
            var cartItem = cartVM.Items.FirstOrDefault(i => i.ProductID == id);

            if (cartItem != null)
            {
                cartVM.Items.Remove(cartItem);
                await SaveCartVM(cartVM);
            }
            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCart()
        {
            await RemoveCartVM();
            return RedirectToAction("Index");
        }

        private async Task RemoveCartVM()
        {
            await Task.Run(() => Response.Cookies.Delete("cart"));
        }
    }
}