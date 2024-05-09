using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Tienda_Online.Models;
using Tienda_Online.Services;
using Tienda_Online.ViewModels;

namespace Tienda.Controllers
{
    public class BaseController : Controller
    {
        public readonly ApplicationDbContext _context;

        public BaseController(ApplicationDbContext context)
        {
            _context = context;
        }
        public override ViewResult View(string? viewNumber, object model)
        {
            ViewBag.NumberProducts = GetCarrito();
            return base.View(viewNumber, model);
        }

        protected int GetCarrito()
        {
            int count = 0;
            string? cartJson = Request.Cookies["cart"];
            if (!string.IsNullOrEmpty(cartJson))
            {
                var cart = JsonConvert.DeserializeObject<List<IdProductAndQuantity>>(cartJson);
                if (cart != null)
                {
                    count = cart.Count;
                }
            }
            return count;
        }

        public async Task<CartVM> AddToCart(int productID, int quantity)
        {
            var product = await _context.Products.FindAsync(productID);

            if (product != null)
            {
                var cartVM = await GetCartVM();

                var cartItem = cartVM.Items.FirstOrDefault(x => x.ProductID == productID);

                if (cartItem != null)
                {
                    cartItem.Quantity += quantity;
                }
                else
                {
                    cartVM.Items.Add(new CartItemVM
                    {
                        ProductID = productID,
                        ProductName = product.ProductName,
                        ProductPrice = product.ProductPrice,
                        Quantity = quantity
                    });
                }
                cartVM.Total = cartVM.Items.Sum(x => x.Quantity * x.ProductPrice);

                await SaveCartVM(cartVM);

                return cartVM;
            }

            return new CartVM();
        }

        public async Task SaveCartVM(CartVM cartVM)
        {
            var productsID = cartVM.Items.Select(x => new IdProductAndQuantity
            {
                IdProduct = x.ProductID,
                Quantity = x.Quantity
            }).ToList();

            var cartJson = await Task.Run(()=> JsonConvert.SerializeObject(productsID));

            Response.Cookies.Append("cart", cartJson, new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(30)
            });
        }

        public async Task<CartVM> GetCartVM()
        {
            var cartJson = Request.Cookies["cart"];

            if (string.IsNullOrEmpty(cartJson))
            {
                return new CartVM();
            }
            else
            {
                var productIDAndQuantity = JsonConvert.DeserializeObject<List<IdProductAndQuantity>>(cartJson);

                var cartVM = new CartVM();

                if (productIDAndQuantity != null)
                {
                    foreach(var item in productIDAndQuantity)
                    {
                        var product = await _context.Products.FindAsync(item.IdProduct);

                        if(product != null)
                        {
                            cartVM.Items.Add(new CartItemVM
                            {
                                ProductID = product.IdProduct,
                                ProductName = product.ProductName,
                                ProductPrice = product.ProductPrice,
                                Quantity = item.Quantity,
                                ProductImage = product.ProductImage
                            });
                        }
                    }
                }
                cartVM.Total = cartVM.Items.Sum(x => x.Total);

                return cartVM;
            }
        }
    }
}