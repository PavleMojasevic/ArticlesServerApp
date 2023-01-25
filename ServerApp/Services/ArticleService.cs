using AutoMapper;
using ServerApp.DTO;
using ServerApp.Infrastucture;
using ServerApp.Interfaces;
using ServerApp.Models;

namespace ServerApp.Services
{
    public class ArticleService : IArticleService
    {
        private VoziNaStrujuDbContext _DbContext;

        public ArticleService(VoziNaStrujuDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task<bool> Add(Article article)
        { 
            _DbContext.Articles.Add(article);
            return true;
        }

        public async Task<bool> AddComment(Comment comment)
        {
            _DbContext.Comments.Add(comment);
            return true;
        }

        public async Task<bool> DeleteById(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Article>> Get()
        { 
            return _DbContext.Articles.ToList();
        }

        public async Task<Article?> GetById(long id)
        {
            Article? article = await _DbContext.Articles.FindAsync(id);
            return article;
        }

        public async Task<bool> Update(long id, Article article)
        {
            throw new NotImplementedException();
        }
    }
}
