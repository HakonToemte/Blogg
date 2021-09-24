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
    public class AdminModel : PageModel
    {
        public List<BlogPost> BlogPosts;
        private IBlogPostProvider _provider {get;}
        private IUserProvider _userprovider {get;}
        public User LoggedUser{get; set;}
        public AdminModel(IBlogPostProvider Provider, IUserProvider Userprovider)
        {
            _provider = Provider;
            _userprovider = Userprovider;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetString("_Name") != null)
            {
                LoggedUser = await _userprovider.GetUser(HttpContext.Session.GetString("_Name"));
            }
            var blogposts = await _provider.GetPostsMadeBy(LoggedUser);
            BlogPosts = blogposts.ToList();
            return Page();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var contact = await _provider.GetBlogPost(id);
            if (contact != null)
            {
                await _provider.RemoveBlogPost(id);
            }

            return RedirectToPage();
        }
    }
}