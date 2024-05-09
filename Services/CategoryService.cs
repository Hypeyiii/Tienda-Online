using Microsoft.EntityFrameworkCore;
using Tienda_Online.Models;

namespace Tienda_Online.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _context.Category.ToListAsync();
        }
    }
}
