using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tienda_Online.Models
{
    public class Order_Details
    {
        [Key]
        [Required]
        public int IdOrder { get; set; }

        [ForeignKey("IdOrder")]
        public Order Order { get; set; } = null!;

        [Required]
        public int IdProduct { get; set; }

        [ForeignKey("IdProduct")]
        public Product Product { get; set; } = null!;

        [Required]
        public int Quantity { get; set; }

        [Required]

        public decimal Price { get; set; }
    }
}
