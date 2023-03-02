using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServerApp.DTO;
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

        public async Task<bool> AddDislikeAsync(long commentId, long userId)
        {
            Comment? comment = await _DbContext.Comments.Include(x => x.Liked).Include(x => x.Disliked).FirstOrDefaultAsync(x => x.Id == commentId);
            if (comment == null) return false;

            User? user = await _DbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) return false;


            if (comment.Disliked.Any(x => x.Id == userId))
                return false;
            comment.Disliked.Add(user);
            comment.Dislikes = comment.Disliked.Count;
            if (comment.Liked.Any(x => x.Id == userId))
            {

                comment.Liked.Remove(user);
                comment.Likes = comment.Liked.Count;
            }
            await _DbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddLikeAsync(long commentId, long userId)
        {
            Comment? comment = await _DbContext.Comments.Include(x => x.Liked).Include(x => x.Disliked).FirstOrDefaultAsync(x => x.Id == commentId);
            if (comment == null) return false;

            User? user = await _DbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) return false;


            if(comment.Liked.Any(x=>x.Id == userId)) 
                return false;
            comment.Liked.Add(user);
            comment.Likes = comment.Liked.Count;
            if(comment.Disliked.Any(x=>x.Id == userId))
            {

                comment.Disliked.Remove(user);
                comment.Dislikes = comment.Disliked.Count;
            }
            await _DbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ApproveAsync(long commentId)
        {
            Comment? comment = await _DbContext.Comments.Include(x => x.Liked).FirstOrDefaultAsync(x => x.Id == commentId);
            if (comment == null) return false;
            comment.Status = CommentStatus.Approved;
            await _DbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<CommentDTO>> GetAsync(long userId)
        {
            var comments = _DbContext.Comments.Include(x => x.Liked).Include(x => x.Disliked).Where(x => x.AuthorId == userId);


            return await comments.Select(x=>new CommentDTO
            {
                Id = x.Id,
                AuthorId = x.AuthorId,
                Date= x.Date,
                DislikedUser=x.Disliked.Any(x=>x.Id==userId),
                LikedUser=x.Liked.Any(x=>x.Id==userId),
                Text=x.Text,
                Likes= x.Likes,
                Dislikes= x.Dislikes,
            }).ToListAsync();
        }

        public async Task<List<CommentDTO>> GetByArticleAsync(long articleId, long userId)
        {
            var comments = _DbContext.Comments.Include(x => x.Replies).Include(x => x.Liked).Include(x => x.Disliked).Where(x => x.ArticleId == articleId);


            return await comments.Select(x => new CommentDTO
            {
                Id = x.Id,
                AuthorId = x.AuthorId,
                Date = x.Date,
                DislikedUser = x.Disliked.Any(x => x.Id == userId),
                LikedUser = x.Liked.Any(x => x.Id == userId), 
                Text = x.Text,
                Likes = x.Likes,
                Dislikes = x.Dislikes,
            }).ToListAsync();
        }

        public async Task<bool> RejectAsync(long commentId)
        {
            Comment? comment = await _DbContext.Comments.Include(x => x.Liked).FirstOrDefaultAsync(x => x.Id == commentId);
            if (comment == null) return false;
            comment.Status = CommentStatus.Rejected;
            await _DbContext.SaveChangesAsync();
            return true;
        }

        public Task<bool> RemoveDislikeAsync(long commentId, long userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveLikeAsync(long commentId, long userId)
        {
            throw new NotImplementedException();
        }
    }
}
