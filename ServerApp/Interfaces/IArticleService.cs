using ServerApp.DTO;
using ServerApp.Models;

namespace ServerApp.Interfaces
{
    public interface IArticleService : IService<Article>
    {
        Task<bool> AddComment(Comment comment);
    }
}
