using Tienda_Online.Models;

namespace Tienda_Online{
    public class ProductsByCategoryVM
    {
        public List<Product> Products { get; set; } = null!;
        public string Search { get; set; } = null!;
        public int? CategoryID { get; set; }

        public string CategoryName { get; set; } = null!;
    }
}