using Tienda_Online.Models;
using Tienda_Online.ViewModels;

namespace Tienda_Online{
    public class GoToShippingVM {
        public CartVM Cart { get; set; } = null!;

        public List<Location> Locations { get; set; } = null!;

        public int LocationID { get; set; }

        public List<User> Users { get; set; } = null!;
    }
}