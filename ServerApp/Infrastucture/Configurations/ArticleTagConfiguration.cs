using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerApp.Models;

namespace ServerApp.Infrastucture.Configurations;

public class ArticleTagConfiguration : IEntityTypeConfiguration<ArticleTag>
{
    public void Configure(EntityTypeBuilder<ArticleTag> builder)
    {
        builder.HasKey(x => new { x.ArticleId, x.TagName });

        builder.HasOne(x => x.Article)
            .WithMany(x => x.Tags)
            .HasForeignKey(x => x.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Tag)
            .WithMany(x => x.Articles)
            .HasForeignKey(x => x.TagName)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
