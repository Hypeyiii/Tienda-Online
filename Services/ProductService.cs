using Microsoft.EntityFrameworkCore;
using Tienda_Online.Models;

namespace Tienda_Online.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetMainProducts()
        {
            IQueryable<Product> products = _context.Products;

            List<Product> productsList = await products.Take(100).ToListAsync();

            return productsList;
        }

        public Product GetProduct(int id)
        {
            var product = _context.Products.Include(p => p.Category).FirstOrDefault(p => p.IdProduct == id);

            if(product != null)
            {
                return product;
            }
            return new Product();
        }

    }
}
