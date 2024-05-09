using Tienda_Online.Models;

namespace Tienda_Online.Services
{
    public interface IProductService
    {
        Product GetProduct(int id);

        Task<List<Product>> GetMainProducts();

    }
}
