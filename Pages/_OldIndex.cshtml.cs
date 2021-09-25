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
    public class OldIndexModel : PageModel
    {
        public List<Post> Posts;
        public string LoggedUser;
        private IPostProvider _postprovider {get;}
        public OldIndexModel(IPostProvider Provider)
        {
            _postprovider = Provider;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var posts = await _postprovider.GetPosts();
            Posts = posts.ToList();
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("_Name")))
            {
                LoggedUser = HttpContext.Session.GetString("_Name");
            }
            return Page();
        }
    }
}
