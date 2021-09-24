using System.Threading.Tasks;
using System.Collections.Generic;

namespace Blogg
{
    public interface IPostProvider
    {
        Task<Post[]> GetPosts();
        Task<Post[]> GetPostsMadeBy(Blog blog);
        Task AddPost(Post post);
        Task<Post> GetPost(int id);
        Task UpdatePost(int id, Post post);
        Task RemovePost(int id);
    }
}
