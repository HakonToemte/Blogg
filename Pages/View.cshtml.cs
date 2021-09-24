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
    public class ViewModel : PageModel
    {
        private readonly IBlogPostProvider _provider;
        public User Author;
        public string LoggedUser;

        public ViewModel(IBlogPostProvider Provider)
        {
            _provider = Provider;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetString("_Name") != null)
            {
                LoggedUser = HttpContext.Session.GetString("_Name");
            }
            if (id == null)
            {
                return NotFound();
            }

            Post = await _provider.GetBlogPost(id.Value);
            Author = Post.User;
            
            if (Post == null)
            {
                return NotFound();
            }
            return Page();
        }
        [BindProperty]
        public BlogPost Post {get; set; }

    }

}
