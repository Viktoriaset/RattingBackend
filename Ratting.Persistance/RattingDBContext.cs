using Microsoft.EntityFrameworkCore;
using Ratting.Aplication.Interfaces;
using Ratting.Domain;
using Ratting.Persistance.EntityTypeConfigurations;

namespace Ratting.Persistance
{
    public class RattingDbContext : DbContext, IRattingDBContext
    {
        public DbSet<Player> players { get; set; }

        public RattingDbContext(DbContextOptions<RattingDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=E:\\UnityProjects\\Ratting\\Server\\Ratting.Persistance\\ratting.sqlite");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PlayerConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public Task<int> SaveChangeAsync(CancellationToken cancellationToken)
        {
            return base.SaveChangesAsync();
        }
    }
}
