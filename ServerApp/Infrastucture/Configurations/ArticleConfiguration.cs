using Microsoft.EntityFrameworkCore;
using ServerApp.Models;

namespace ServerApp.Infrastucture.Configurations;

public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Article> builder)
    {
        builder.HasKey(x => x.Id); 
        builder.Property(x => x.Id).ValueGeneratedOnAdd(); 

        builder.HasMany(x => x.Comments).WithOne(x => x.Article).HasForeignKey(x=>x.ArticleId).OnDelete(DeleteBehavior.Cascade);  
        builder.HasMany(x => x.Tags).WithMany(x => x.Articles); 
    }
}
