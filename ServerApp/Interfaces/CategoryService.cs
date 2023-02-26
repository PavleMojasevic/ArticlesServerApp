using ServerApp.DTO;
using ServerApp.Models;

namespace ServerApp.Interfaces
{
    public interface ICategoryService
    {
        Task<bool> AddAsync(Category category);
        Task<bool> EditAsync(long categoryId, CategoryDTO category);
        Task<List<Category>> GetAsync();
    }
}
