using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServerApp.DTO;
using ServerApp.Infrastucture;
using ServerApp.Interfaces;
using ServerApp.Models;

namespace ServerApp.Services;

public class ArticleService : IArticleService
{
    private ArticlesDbContext _DbContext;

    public ArticleService(ArticlesDbContext dbContext)
    {
        _DbContext = dbContext;
    }

    public async Task<bool> AddAsync(Article article)
    {
        if (article.Image?.Length == 0) return false;
        if (string.IsNullOrWhiteSpace(article.Title)) return false;
        if (article.CategoryId == 0) return false;


        if (_DbContext.Articles.Any(x => x.Title == article.Title)) return false;

        article.CategoryId = article.Category.Id;
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


    public async Task<List<Article>> GetAsync(ArticleFilterDTO? filter = null)
    {

        IQueryable<Article> articles = _DbContext.Articles;
        if (filter != null)
        { 
            if (filter.AuthorId != null)
            {
                articles = articles.Where(x => x.AuthorId == filter.AuthorId);
            }
            if (filter.TagId != null)
            {
                articles = articles.Where(x => x.Tags.Any(y => y.Id == filter.TagId));
            } 
            if (filter.CategoryId != null)
            {
                articles = articles.Where(x => x.CategoryId == filter.CategoryId);
            } 
        }
        return await articles.ToListAsync();
    }

    public async Task<Article?> GetByIdAsync(long id)
    {
        Article? article = await _DbContext.Articles.FirstOrDefaultAsync(x=>x.Id==id);
        return article;
    }

    public async Task<bool> UpdateAsync(long id, EditArticeDto article)
    {
        Article? articleFromDb = await _DbContext.Articles.FirstOrDefaultAsync(x => x.Id == id);
        if (articleFromDb == null)
        {
            return false;
        }
        if (article.CategoryId != null)
        {
            articleFromDb.CategoryId = (long)article.CategoryId;
        }
        if (article.Content != null)
        {
            articleFromDb.Content = article.Content;
        }
        if (article.Image != null)
        {
            articleFromDb.Image = article.Image;
        }
        await _DbContext.SaveChangesAsync();
        return true;
    }
}
