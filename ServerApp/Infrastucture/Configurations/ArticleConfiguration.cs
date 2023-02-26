using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerApp.Models;

namespace ServerApp.Infrastucture.Configurations;

public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.HasKey(x => x.Id); 
        builder.Property(x => x.Id).ValueGeneratedOnAdd(); 

        builder.HasMany(x => x.Comments)
            .WithOne(x => x.Article)
            .HasForeignKey(x=>x.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);
         

    }
}
