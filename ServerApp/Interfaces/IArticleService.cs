using ServerApp.DTO;
using ServerApp.Models;

namespace ServerApp.Interfaces;

public interface IArticleService : IService<Article>
{
    Task<bool> AddCommentAsync(Comment comment);
}
