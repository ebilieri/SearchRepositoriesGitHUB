using Microsoft.EntityFrameworkCore;
using SearchRepositoriesGitHUB.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchRepositoriesGitHUB.Repositories
{
    public class SearchRepositoriesGitHUBDBContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        //public DbSet<Owner> Owners { get; set; }
        //public DbSet<License> Licenses { get; set; }


        public SearchRepositoriesGitHUBDBContext(DbContextOptions<SearchRepositoriesGitHUBDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasKey(p => p.Id);
            modelBuilder.Entity<Owner>().HasKey(p => p.Id);
            //modelBuilder.Entity<License>().HasKey(p => p.NodeId);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
    }
}
