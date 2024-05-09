using Tienda_Online.Models;

namespace Tienda_Online.ViewModels
{
    public class CartItemVM
    {
        public int ProductID { get; set; }
        public Product Product { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string ProductImage { get; set; } = null!;
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Total => ProductPrice * Quantity;
    }
}
