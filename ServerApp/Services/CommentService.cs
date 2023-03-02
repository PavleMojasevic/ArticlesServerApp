using Microsoft.EntityFrameworkCore;
using ServerApp.Infrastucture;
using ServerApp.Interfaces;
using ServerApp.Models;

namespace ServerApp.Services
{
    public class CommentService : ICommentService
    {
        private ArticlesDbContext _DbContext;

        public CommentService(ArticlesDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public async Task<bool> AddAsync(Comment comment)
        {
            if (string.IsNullOrWhiteSpace(comment.Text))
                return false;

            await _DbContext.Comments.AddAsync(comment);
            await _DbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddDislikeAsync(int commentId, long userId)
        {
            Comment? comment = await _DbContext.Comments.Include(x => x.Disliked).FirstOrDefaultAsync(x => x.Id == commentId);
            if (comment == null) return false;

            User? user = await _DbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) return false;

            comment.Disliked.Add(user);
            comment.Dislikes = comment.Disliked.Count;
            await _DbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddLikeAsync(int commentId, long userId)
        {
            Comment? comment = await _DbContext.Comments.Include(x => x.Liked).FirstOrDefaultAsync(x => x.Id == commentId);
            if (comment == null) return false;

            User? user = await _DbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) return false;

            comment.Liked.Add(user);
            comment.Likes = comment.Liked.Count;
            await _DbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ApproveAsync(int commentId)
        {
            Comment? comment = await _DbContext.Comments.Include(x => x.Liked).FirstOrDefaultAsync(x => x.Id == commentId);
            if (comment == null) return false;
            comment.Status = CommentStatus.Approved;
            await _DbContext.SaveChangesAsync();
            return true;
        }

        public Task<List<Comment>> GetAsync(long userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Comment>> GetByArticleAsync(long articleId)
        {
            return await _DbContext.Comments.Where(x=>x.ArticleId==articleId).ToListAsync();
        }

        public async Task<bool> RejectAsync(int commentId)
        {
            Comment? comment = await _DbContext.Comments.Include(x => x.Liked).FirstOrDefaultAsync(x => x.Id == commentId);
            if (comment == null) return false;
            comment.Status = CommentStatus.Rejected;
            await _DbContext.SaveChangesAsync();
            return true;
        }

        public Task<bool> RemoveDislikeAsync(int commentId, long userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveLikeAsync(int commentId, long userId)
        {
            throw new NotImplementedException();
        }
    }
}
