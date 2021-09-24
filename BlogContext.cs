using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System; 
using System.Data;
using System.Threading.Tasks;
using Blogg;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;


namespace Blogg
{
    // This class should inherit from the EntityFramework DbContext
    public class BlogContext : DbContext
    {    
        public string DbPath { get; private set; }
        public BlogContext(DbContextOptions<BlogContext> options)
            :base(options)
        {
            DbPath = "sqlitedb1";
        }
        public DbSet<BlogPost> BlogPosts {get; set; }
        public DbSet<User> Users {get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogPost>().ToTable("blogposts");
            modelBuilder.Entity<BlogPost>(entity => {
                 entity.HasKey(k=>k.Id);
                 entity.HasIndex(i=>i.Title).IsUnique();
            });
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<User>(entity => {
                 entity.HasKey(f=>f.Id);
                 entity.HasIndex(g=>g.Name).IsUnique();
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
