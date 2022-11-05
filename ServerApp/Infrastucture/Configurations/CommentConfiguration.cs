using Microsoft.EntityFrameworkCore;
using ServerApp.Models;

namespace ServerApp.Infrastucture.Configurations
{
    public class CommentConfiguration:IEntityTypeConfiguration<Comment>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasMany(x=>x.Replies).WithOne(x=>x.Parent).HasForeignKey(x=>x.ParentId);


        }
    }
}
