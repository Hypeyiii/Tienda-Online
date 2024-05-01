using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tienda_Online.Models
{
    public class Product
    {
        [Key]
        [Required]
        public int IdProduct { get; set; }

        [Required]
        public string? ProductName { get; set; }

        [Required]
        public string? ProductDescription { get; set; }

        [Required]
        public string? ProductImage { get; set; }

        [Required]
        public decimal ProductPrice { get; set; }

        [Required]
        public int IdCategory { get; set; }

        [Required]
        public Category Category { get; set; } = null!;

        [Required]
        public int Stock { get; set; }

        public ICollection<Order_Details> OrderDetails { get; set; } = null!;

    }
}