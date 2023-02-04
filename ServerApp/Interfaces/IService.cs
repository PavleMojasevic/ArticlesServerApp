using ServerApp.Models;

namespace ServerApp.Interfaces
{
    public interface IService<T> where T : class
    {
        Task<List<T>> Get();
        Task<T?> GetById(long id);
        Task<bool> Add(T entity);
        Task<bool> Update(long id, T entity);
    }
}   
