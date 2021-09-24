using System.Threading.Tasks;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Blogg
{
    public class PostProvider : IPostProvider
    {
        private readonly BlogContext _blogContext;

        public PostProvider(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public async Task<Post[]> GetPosts()
        {
            var list = _blogContext.Posts.Where(b => b.Id == b.Id).Include(b=>b.Blog);
            Post[] new_list = list.ToArray();
            return await Task.Run(() => new_list);
        }
        public async Task<Post[]> GetPostsMadeBy(Blog blog)
        {
            var list = _blogContext.Posts.Where(b => b.Blog == blog).Include(b=>b.Blog);
            Post[] new_list = list.ToArray();
            return await Task.Run(() => new_list);
        }

        public Task AddPost(Post post){
            _blogContext.Posts.Add(post);
            _blogContext.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<Post> GetPost(int id){
            var post = _blogContext.Posts.Where(b => b.Id == id).Include(b=>b.Blog).FirstOrDefault();
            return await Task.Run(() => post);
        }

        public Task UpdatePost(int id, Post post)
        {
            _blogContext.Posts.Update(post);
            _blogContext.SaveChanges();
            
            return Task.CompletedTask;
        }

        public Task RemovePost(int id)
        {
            _blogContext.Posts.Remove(_blogContext.Posts.Find(id));
            _blogContext.SaveChanges();
            return Task.CompletedTask;
        }

        
    }
}
