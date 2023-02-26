using Microsoft.AspNetCore.Mvc;
using ServerApp.DTO;
using ServerApp.Models;

namespace ServerApp.Interfaces
{
    public interface ICommentService
    {
        Task<bool> AddAsync(Comment comment);
        Task<bool> AddDislikeAsync(int commentId, long userId);
        Task<bool> AddLikeAsync(int commentId, long userId);
        Task<bool> ApproveAsync(int commentId);
        Task<List<Comment>> GetAsync(long userId);
        Task<List<Comment>> GetByArticleAsync(long? userId, long articleId);
        Task<bool> RejectAsync(int commentId);
        Task<bool> RemoveDislikeAsync(int commentId, long userId);
        Task<bool> RemoveLikeAsync(int commentId, long userId);
    }
}
