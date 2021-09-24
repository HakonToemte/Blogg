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
    public class IndexModel : PageModel
    {
        public List<BlogPost> BlogPosts;
        public string LoggedUser;
        private IBlogPostProvider _provider {get;}
        public IndexModel(IBlogPostProvider Provider)
        {
            _provider = Provider;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var blogposts = await _provider.GetPosts();
            BlogPosts = blogposts.ToList();
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("_Name")))
            {
                LoggedUser = HttpContext.Session.GetString("_Name");
            }
            return Page();
        }
    }
}
