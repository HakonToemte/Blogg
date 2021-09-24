using System.Threading.Tasks;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Blogg
{
    public class BlogProvider : IBlogProvider
    {
        private readonly BlogContext _blogContext;

        public BlogProvider(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public async Task<Blog[]> GetBlogs()
        {
            var list = await _blogContext.Blogs.ToListAsync();
            Blog[] new_list = list.ToArray();
            return new_list;
        }

        public Task AddBlog(Blog blog){
            _blogContext.Blogs.Add(blog);
            _blogContext.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<Blog> GetBlog(string name){
            var blog = _blogContext.Blogs.Where(b => b.UserName == name)
                    .FirstOrDefault();
            if (blog == null)
            {
                return null;
            }
            return await Task.Run(() => blog);
        }

        public Task UpdateBlog(int id, Blog blog)
        {
            _blogContext.Blogs.Update(blog);
            _blogContext.SaveChanges();
            
            return Task.CompletedTask;
        }

        public Task RemoveBlog(int id)
        {
            _blogContext.Blogs.Remove(_blogContext.Blogs.Find(id));
            _blogContext.SaveChanges();
            return Task.CompletedTask;
        }

        
    }
}