using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerApp.Models;

namespace ServerApp.Infrastucture.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{ 
    public void Configure(EntityTypeBuilder<Category> builder)
    { 
        builder.HasKey(x => x.Id); 
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasMany(x=>x.Articles)
            .WithOne(x=>x.Category)
            .HasForeignKey(x=>x.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(x => x.Subcategories)
            .WithOne(x => x.Parent)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
