using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tienda_Online.Services;

namespace Tienda_Online.Models
{
    public class User
    {
        public User()
        {
            Orders = new List<Order>();
        }
        [Key]
        [Required]
        public int UserID { get; set; }
        [Required]
        public string FirstName { get; set; } = "";
        [Required]
        public string LastName { get; set; } = "";
        [Required]
        public string Password { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
        [Required]
        public string UserName { get; set; } = "";
        [Required]
        public string Address { get; set; } = "";
        [Required]
        public string City { get; set; } = "";
        [Required]
        public int PostalCode { get; set; }
        public ICollection<Order> Orders { get; set; } = null!;
        [InverseProperty("User")]

        public ICollection<Location> Location { get; set; } = null!;
    }
}
