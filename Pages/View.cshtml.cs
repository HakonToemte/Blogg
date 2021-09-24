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
        private readonly IPostProvider _postprovider;
        public Blog Author;
        public string LoggedUser;

        public ViewModel(IPostProvider Provider)
        {
            _postprovider = Provider;
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

            Post = await _postprovider.GetPost(id.Value);
            Author = Post.Blog;
            
            if (Post == null)
            {
                return NotFound();
            }
            return Page();
        }
        [BindProperty]
        public Post Post {get; set; }

    }

}
