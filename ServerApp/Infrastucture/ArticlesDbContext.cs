using Microsoft.EntityFrameworkCore;
using ServerApp.Models;

namespace ServerApp.Infrastucture;

public class ArticlesDbContext:DbContext
{

    public DbSet<Article> Articles { get; set; }
    public DbSet<Comment> Comments { get; set; } 
    public DbSet<Tag> Tags { get; set; }
    public DbSet<User> Users{ get; set; }

    public ArticlesDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArticlesDbContext).Assembly);
    }
}
