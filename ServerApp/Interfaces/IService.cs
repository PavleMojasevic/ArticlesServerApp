using ServerApp.Models;

namespace ServerApp.Interfaces;

public interface IService<T> where T : class
{
    Task<List<T>> GetAsync();
    Task<T?> GetByIdAsync(long id);
    Task<bool> Add(T entity);
    Task<bool> UpdateAsync(long id, T entity);
}

