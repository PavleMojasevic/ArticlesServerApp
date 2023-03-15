using ServerApp.DTO;
using ServerApp.Models;

namespace ServerApp.Interfaces;

public interface IArticleService
{
    Task<List<Article>> GetAsync(ArticleFilterDTO? filter = null);
    Task<Article?> GetByIdAsync(long id);
    Task AddAsync(Article entity);
    Task<bool> UpdateAsync(long id, EditArticeDto entity);
}
