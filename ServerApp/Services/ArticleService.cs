using AutoMapper;
using ServerApp.DTO;
using ServerApp.Infrastucture;
using ServerApp.Interfaces;
using ServerApp.Models;

namespace ServerApp.Services
{
    public class ArticleService : IArticleService
    {
        private ArticlesDbContext _DbContext;

        public ArticleService(ArticlesDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task<bool> Add(Article article)
        { 
            if(article.Image?.Length==0) return false;
            if (string.IsNullOrWhiteSpace(article.Title)) return false;
            if (article.Category==null)return false;

             
            if (_DbContext.Articles.Any(x => x.Title == article.Title)) return false;

            article.CategoryId=article.Category.Id;
            article.Comments = new List<Comment>();
            article.Date = DateTime.Now;
            
            await _DbContext.Articles.AddAsync(article);
            return true;
        }

        public async Task<bool> AddCommentAsync(Comment comment)
        {
            await _DbContext.Comments.AddAsync(comment);
            return true;
        }


        public async Task<List<Article>> GetAsync()
        { 
            return _DbContext.Articles.ToList();
        }

        public async Task<Article?> GetByIdAsync(long id)
        {
            Article? article = await _DbContext.Articles.FindAsync(id);
            return article;
        }

        public async Task<bool> UpdateAsync(long id, Article article)
        {
            throw new NotImplementedException();
        }
    }
}
