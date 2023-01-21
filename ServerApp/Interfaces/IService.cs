using ServerApp.Models;

namespace ServerApp.Interfaces
{
    public interface IService<T>
    {
        Task<List<T>> Get();
        Task<T?> GetById(int id);
        Task<bool> Add(T entity);
        Task<bool> Update(int id, T entity);
        Task<bool> DeleteById(int id);
    }
}
