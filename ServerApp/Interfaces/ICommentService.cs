using Microsoft.AspNetCore.Mvc;
using ServerApp.DTO;
using ServerApp.Models;

namespace ServerApp.Interfaces;

public interface ICommentService
{
    Task<bool> AddAsync(Comment comment);
    Task<bool> AddDislikeAsync(long commentId, long userId);
    Task<bool> AddLikeAsync(long commentId, long userId);
    Task<bool> ApproveAsync(long commentId);
    Task<List<CommentDTO>> GetAllAsync();
    Task<List<CommentDTO>> GetAsync(long userId);
    Task<List<CommentDTO>> GetByArticleAsync(long articleId, long userId);
    Task<bool> RejectAsync(long commentId);
    Task<bool> RemoveDislikeAsync(long commentId, long userId);
    Task<bool> RemoveLikeAsync(long commentId, long userId);
}
