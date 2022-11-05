using Microsoft.EntityFrameworkCore;
using ServerApp.Models;

namespace ServerApp.Infrastucture
{
    public class VoziNaStrujuDbContext:DbContext
    {

        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public VoziNaStrujuDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Kazemo mu da pronadje sve konfiguracije u Assembliju i da ih primeni nad bazom
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VoziNaStrujuDbContext).Assembly);
        }
    }
}
