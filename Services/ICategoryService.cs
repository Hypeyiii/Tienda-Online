using Tienda_Online.Models;

namespace Tienda_Online.Services
{
    public interface ICategoryService
    {
        Task <List<Category>> GetCategories();
    }
}
