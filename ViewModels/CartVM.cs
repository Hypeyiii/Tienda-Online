namespace Tienda_Online.ViewModels
{
    public class CartVM
    {
        public List<CartItemVM> Items { get; set; } = new List<CartItemVM>();
        public decimal Total { get; set; }
    }
}
