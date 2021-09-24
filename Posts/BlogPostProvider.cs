using System.Threading.Tasks;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Blogg
{
    public class BlogPostProvider : IBlogPostProvider
    {
        private readonly BlogContext _blogContext;

        public BlogPostProvider(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public async Task<BlogPost[]> GetPosts()
        {
            var list = _blogContext.BlogPosts.Where(b => b.Id == b.Id).Include(b=>b.User);
            BlogPost[] new_list = list.ToArray();
            return await Task.Run(() => new_list);
        }
        public async Task<BlogPost[]> GetPostsMadeBy(User user)
        {
            var list = _blogContext.BlogPosts.Where(b => b.User == user).Include(b=>b.User);
            BlogPost[] new_list = list.ToArray();
            return await Task.Run(() => new_list);
        }

        public Task AddBlogPost(BlogPost post){
            _blogContext.BlogPosts.Add(post);
            _blogContext.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<BlogPost> GetBlogPost(int id){
            var u = _blogContext.BlogPosts.Where(b => b.Id == id).Include(b=>b.User).FirstOrDefault();
            return await Task.Run(() => u);
        }

        public Task UpdateBlogPost(int id, BlogPost post)
        {
            _blogContext.BlogPosts.Update(post);
            _blogContext.SaveChanges();
            
            return Task.CompletedTask;
        }

        public Task RemoveBlogPost(int id)
        {
            _blogContext.BlogPosts.Remove(_blogContext.BlogPosts.Find(id));
            _blogContext.SaveChanges();
            return Task.CompletedTask;
        }

        
    }
}
