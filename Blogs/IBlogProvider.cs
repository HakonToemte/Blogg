using System.Threading.Tasks;
using System.Collections.Generic;

namespace Blogg
{
    public interface IBlogProvider
    {
        Task<ICollection<Blog>> GetBlogs();
        Task AddBlog(Blog blog);
        Task<Blog> GetBlog(string name);
        Task UpdateBlog(int id, Blog blog);
        Task RemoveBlog(int id);
    }
}
