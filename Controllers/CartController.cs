using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tienda.Controllers;
using Tienda_Online.Services;
using Tienda_Online.Models;
using Newtonsoft.Json;
using Tienda_Online.ViewModels;
using Microsoft.EntityFrameworkCore;
using PayPalCheckoutSdk.Orders;
using PayPalCheckoutSdk.Core;
using PayPalHttp;

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
                cartVM.Total = cartVM.Items.Sum(item => item.Total);
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

        private readonly string clientID = "AWb3zb3RIR6GpqAICKf07AWeSz8FXXjzCWRVkwFBRBJDw2FvfI0BbxzNkbEQ_kiZZoZYFSHTgtObYt_U";

        private readonly string clientSecret = "EILS0VH6OY1Z-2ULWgBQxWFZa-3YnBq6M2luVV0yarJnw7ZjE7LEbfCPz-SRqN3KsLfk2I6FkfvT8jvA";

        public IActionResult SuccessWithPaypal(decimal total)
        {
            var request = new OrdersCreateRequest();
            request.Prefer("return=representation");
            request.RequestBody(BuildRequestBody(total));

            var environment = new SandboxEnvironment(clientID, clientSecret);

            var client = new PayPalHttpClient(environment);

            try
            {
                var response = client.Execute(request).Result;
                var statusCode = response.StatusCode;
                var responseBody = response.Result<PayPalCheckoutSdk.Orders.Order>();

                var appToLink = responseBody.Links.FirstOrDefault(link => link.Rel == "approve");

                if (appToLink != null)
                {
                    return Redirect(appToLink.Href);
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            catch (HttpException e)
            {
                ;
                return (IActionResult)e;
            }
        }

        private OrderRequest BuildRequestBody(decimal total)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";

            var request = new OrderRequest()
            {
                CheckoutPaymentIntent = "CAPTURE",
                PurchaseUnits = new List<PurchaseUnitRequest>()
                {
                    new PurchaseUnitRequest()
                    {
                        AmountWithBreakdown = new AmountWithBreakdown()
                        {
                            CurrencyCode = "MXN",
                            Value = total.ToString("F2").Replace(",", ".")
                        }
                    }
                },
                ApplicationContext = new ApplicationContext()
                {
                    ReturnUrl = $"{baseUrl}/Cart/PaymentSuccess",
                    CancelUrl = $"{baseUrl}/Cart/Index"
                }
            };
            return request;
        }

        public IActionResult PaymentSuccess()
        {
            try
            {
                var cartJson = Request.Cookies["cart"];

                List<IdProductAndQuantity>? productAndQuantities = !string.IsNullOrEmpty(cartJson) ? JsonConvert.DeserializeObject<List<IdProductAndQuantity>>(cartJson) : null;

                CartVM cartVM = new();

                if (productAndQuantities != null)
                {
                    foreach (var item in productAndQuantities)
                    {
                        var product = _context.Products.Find(item.IdProduct);
                        if (product != null)
                        {
                            cartVM.Items.Add(new CartItemVM
                            {
                                ProductID = product.IdProduct,
                                Quantity = item.Quantity,
                                ProductPrice = product.ProductPrice,
                                ProductImage = product.ProductImage ?? "",
                            });
                        }
                    }
                }

                var userID = User.Identity?.IsAuthenticated == true ? int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)) : 0;

                cartVM.Total = cartVM.Items.Sum(item => item.Total);

                var order = new Models.Orders
                {
                    UserID = userID,
                    Total = cartVM.Total,
                    State = "Realizado",
                    OrderDate = DateTime.Now,
                };
                _context.Orders.Add(order);
                _context.SaveChanges();

                foreach (var item in cartVM.Items)
                {
                    var OrderDetails = new Order_Details
                    {
                        IdOrder = order.IdOrder,
                        IdProduct = item.ProductID,
                        Quantity = item.Quantity,
                        Price = item.ProductPrice,
                    };
                    _context.OrderDetails.Add(OrderDetails);
                    _context.SaveChanges();


                    var product = _context.Products.FirstOrDefault(p => p.IdProduct == item.ProductID);

                    if (product != null)
                    {
                        product.Stock -= item.Quantity;
                    }
                }
                _context.SaveChanges();

                Response.Cookies.Delete("cart");

                ViewBag.OrderDetails = _context.OrderDetails.Where(od => od.IdOrder == order.IdOrder).ToList();


                return View("PaymentSuccess", order);
            }
            catch (HttpException e)
            {
                ;
                return (IActionResult)e;
            }
        }

        public IActionResult MyOrders()
        {
            var userID = User.Identity?.IsAuthenticated == true ? int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)) : 0;

            var orders = _context.Orders.Where(o => o.UserID == userID).ToList();

            return View(orders);
        }

        public async Task<IActionResult> MyOrderDetails(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order != null)
            {
                var applicationDbContext = _context.OrderDetails
                    .Include(o => o.Order)
                    .Include(o => o.Product)
                    .Where(o => o.IdOrder == id);
                var details = await applicationDbContext.ToListAsync();
                return View(details);
            }
            else
            {
                return RedirectToAction("MyOrders");
            }
        }


        public IActionResult DeleteOrder(int id)
        {
            var order = _context.Orders.Find(id);

            if (order != null)
            {
                _context.OrderDetails.RemoveRange(_context.OrderDetails.Where(od => od.IdOrder == order.IdOrder));
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }

            return RedirectToAction("MyOrders");
        }
    }
}