using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Blogg.Pages
{
    public class EditViewModel : PageModel
    {
        public List<Post> Posts;
        private IPostProvider _postprovider {get;}
        private IBlogProvider _blogprovider {get;}
        public Blog LoggedUser{get; set;}
        public string Error{get; set;}
        public EditViewModel(IPostProvider Provider, IBlogProvider Blogprovider)
        {
            _postprovider = Provider;
            _blogprovider = Blogprovider;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetString("_Name") != null)
            {
                LoggedUser = await _blogprovider.GetBlog(HttpContext.Session.GetString("_Name"));
            }
            var posts = await _postprovider.GetPostsMadeBy(LoggedUser);
            Posts = posts.ToList();
            return Page();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var postToDelete = await _postprovider.GetPost(id);
            if (postToDelete.Blog.UserName != HttpContext.Session.GetString("_Name")){ // If person logged in is not the author of the post.
                Error = "You are not the author of this post";                         // Dont allow them to edit.
                return Page();
            }
            if (postToDelete != null)
            {
                await _postprovider.RemovePost(id);
            }

            return RedirectToPage();
        }
    }
}