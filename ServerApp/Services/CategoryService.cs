using Microsoft.EntityFrameworkCore;
using ServerApp.DTO;
using ServerApp.Infrastucture;
using ServerApp.Interfaces;
using ServerApp.Models;

namespace ServerApp.Services
{
    public class CategoryService : ICategoryService
    {
        private ArticlesDbContext _DbContext;

        public CategoryService(ArticlesDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public async Task<bool> AddAsync(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
                return false;
            await _DbContext.Categories.AddAsync(category);
            await _DbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditAsync(long categoryId, CategoryDTO categoryDTO)
        {

            if (string.IsNullOrWhiteSpace(categoryDTO.Name))
                return false;
            Category? category = await _DbContext.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);
            if (category == null)
                return false;
            category.Name=categoryDTO.Name;
            category.ParentId=categoryDTO.ParentId;
            await _DbContext.SaveChangesAsync(); 
            return true;
        }

        public async Task<List<Category>> GetAsync()
        {
            return await _DbContext.Categories.ToListAsync();   
        }
    }
}
