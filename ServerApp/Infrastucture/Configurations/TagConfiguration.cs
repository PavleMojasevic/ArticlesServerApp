using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerApp.Models;

namespace ServerApp.Infrastucture.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    { 
        public void Configure(EntityTypeBuilder<Tag> builder)
        { 
            builder.HasKey(x => x.Id); 
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
