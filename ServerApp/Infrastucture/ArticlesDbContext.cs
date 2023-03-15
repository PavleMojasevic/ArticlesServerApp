using Microsoft.EntityFrameworkCore;
using ServerApp.Models;

namespace ServerApp.Infrastucture;

public class ArticlesDbContext : DbContext
{

    public virtual DbSet<Article> Articles { get; set; }
    public virtual DbSet<Comment> Comments { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Tag> Tags { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<ArticleTag> ArticleTags { get; set; }

    public ArticlesDbContext(DbContextOptions options) : base(options)
    {
    }

    public ArticlesDbContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArticlesDbContext).Assembly);
    }
}
