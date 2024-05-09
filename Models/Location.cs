using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tienda_Online.Models
{
    public class Location
    {
        [Key]
        [Required]
        public string IdAddress { get; set; } = null!;

        [Required(ErrorMessage = "El nombre de la dirección es obligatorio.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "El nombre de la ciudad es obligatorio.")]
        public string? City { get; set; }

        [Required(ErrorMessage = "El nombre del país es obligatorio.")]
        public string? Country { get; set; }

        [Required(ErrorMessage = "El código postal es obligatorio.")]
        public string? PostalCode { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        [ForeignKey("UserID")]
        public User User { get; set; } = null!;
    }
}

