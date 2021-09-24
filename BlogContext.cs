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
        public DbSet<Post> Posts {get; set; }
        public DbSet<Blog> Blogs {get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().ToTable("posts");
            modelBuilder.Entity<Post>(entity => {
                 entity.HasKey(k=>k.Id);
                 entity.HasIndex(i=>i.Title).IsUnique();
            });
            modelBuilder.Entity<Blog>().ToTable("users");
            modelBuilder.Entity<Blog>(entity => {
                 entity.HasKey(f=>f.UserId);
                 entity.HasIndex(g=>g.UserName).IsUnique();
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
