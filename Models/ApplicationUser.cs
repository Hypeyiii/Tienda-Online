using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tienda_Online.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Orders = new List<Order>();
        }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Address { get; set; } = "";
        public string City { get; set; } = "";
        public DateTime DateTime { get; set; } = DateTime.Now;

        public ICollection<Order> Orders { get; set; } = null!;
        [InverseProperty("User")]

        public ICollection<Location> Location { get; set; } = null!;
    }
}
