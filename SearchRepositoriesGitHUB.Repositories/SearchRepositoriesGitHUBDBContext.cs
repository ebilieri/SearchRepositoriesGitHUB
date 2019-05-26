using Microsoft.EntityFrameworkCore;
using SearchRepositoriesGitHUB.Models;

namespace SearchRepositoriesGitHUB.Repositories
{
    public class SearchRepositoriesGitHUBDBContext : DbContext
    {        
        public DbSet<Repositorio> Repositorios { get; set; }

        public SearchRepositoriesGitHUBDBContext(DbContextOptions<SearchRepositoriesGitHUBDBContext> options) : base(options)
        {

        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.Entity<Repositorio>().HasKey(p => p.IdGitHub);
           
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
    }
}
