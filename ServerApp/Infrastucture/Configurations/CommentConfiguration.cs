using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerApp.Models;

namespace ServerApp.Infrastucture.Configurations;

public class CommentConfiguration:IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(x => x.Id); 
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasMany(x=>x.Replies)
            .WithOne(x=>x.Parent).HasForeignKey(x=>x.ParentId)
            .OnDelete(DeleteBehavior.SetNull); 
    }
}
