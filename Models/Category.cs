using System.ComponentModel.DataAnnotations;

namespace Tienda_Online.Models
{
    public class Category
    {
        public Category()
        {
            Products = new List<Product>();
        }
        [Key]
        [Required]
        public int IdCategory { get; set; }

        [Required (ErrorMessage = "El nombre de la categoria es obligatorio.")]
        public string? CategoryName { get; set; }

        [Required (ErrorMessage = "La descripción de la cateogria es obligatorio.")]
        public string? CategoryDescription { get; set; }

        [Required]
        public ICollection<Product> Products { get; set; } = null!;
    }
}
