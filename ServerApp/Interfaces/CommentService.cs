using Microsoft.AspNetCore.Mvc;
using ServerApp.DTO;
using ServerApp.Models;

namespace ServerApp.Interfaces
{
    public interface ICommentService
    {
        Task<bool> AddAsync(Comment comment);
        Task<bool> AddDislikeAsync(long commentId, long userId);
        Task<bool> AddLikeAsync(long commentId, long userId);
        Task<bool> ApproveAsync(long commentId);
        Task<List<Comment>> GetAsync(long userId);
        Task<List<Comment>> GetByArticleAsync(long articleId);
        Task<bool> RejectAsync(long commentId);
        Task<bool> RemoveDislikeAsync(long commentId, long userId);
        Task<bool> RemoveLikeAsync(long commentId, long userId);
    }
}
