using System.Threading.Tasks;
using System.Collections.Generic;

namespace Blogg
{
    public interface IBlogPostProvider
    {
        Task<BlogPost[]> GetPosts();
        Task<BlogPost[]> GetPostsMadeBy(User user);
        Task AddBlogPost(BlogPost post);
        Task<BlogPost> GetBlogPost(int id);
        Task UpdateBlogPost(int id, BlogPost post);
        Task RemoveBlogPost(int id);
    }
}
