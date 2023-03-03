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

    public async Task AddAsync(Article article)
    { 
        if (string.IsNullOrWhiteSpace(article.Title)) throw new ArgumentException("Title is not valid");
        if (article.CategoryId == 0) throw new ArgumentException("Category id is not valid");


        if (_DbContext.Articles.Any(x => x.Title == article.Title)) throw new ArgumentException("Article with this title already exists");

        article.Comments = new List<Comment>();
        article.Date = DateTime.Now;
        var tags = article.Tags; 
        await _DbContext.Articles.AddAsync(article);

        await _DbContext.SaveChangesAsync(); 
         
    } 
    public async Task<bool> AddCommentAsync(Comment comment)
    {
        await _DbContext.Comments.AddAsync(comment);
        await _DbContext.SaveChangesAsync();
        return true;
    }


    public async Task<List<Article>> GetAsync(ArticleFilterDTO? filter = null)
    {

        IQueryable<Article> articles = _DbContext.Articles.Include(x=>x.Tags);
        if (filter != null)
        { 
            if (filter.AuthorId != null)
            {
                articles = articles.Where(x => x.AuthorId == filter.AuthorId);
            }
            if (filter.Tag != null)
            {
                articles = articles.Where(x => x.Tags.Any(y => y.TagName == filter.Tag));
            } 
            if (filter.CategoryId != null)
            {
                articles = articles.Where(x => x.CategoryId == filter.CategoryId);
            } 
        }
        var result = await articles.ToListAsync(); 

        
        return result;
    }

    public async Task<Article?> GetByIdAsync(long id)
    {
        Article? article = await _DbContext.Articles.Include(x => x.Tags).FirstOrDefaultAsync(x=>x.Id==id); 
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
