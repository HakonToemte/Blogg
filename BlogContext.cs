using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection;    
using System; 
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using BC = BCrypt.Net.BCrypt;


namespace Blogg
{
    // This class should inherit from the EntityFramework DbContext
    public class BlogContext : IdentityDbContext
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
            });
            //modelBuilder.Entity<Blog>().ToTable("users");
            //modelBuilder.Entity<Blog>(entity => {
            //     entity.HasKey(f=>f.Id);
            //});
            base.OnModelCreating(modelBuilder);
        }
    }
}
