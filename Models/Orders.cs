using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tienda_Online.Models
{
    public class Orders
    {
        [Key]
        public int IdOrder { get; set; }

        [Required]
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; } = null!;

        [Required]
        public decimal Total { get; set; }

        [Required]
        public string State { get; set; } = null!;

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required]
        public ICollection<Order_Details> OrderDetails { get; set; } = null!;
    }
}
