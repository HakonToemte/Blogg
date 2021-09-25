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
        private readonly IBlogProvider _blogprovider;
        [BindProperty]
        public ICollection<Blog> Blogs{get;set;}
        public string LoggedUser;

        public IndexModel(IBlogProvider Provider)
        {
            _blogprovider = Provider;
        }

        public async Task<IActionResult> OnGetAsync(string name="")
        {
            if (HttpContext.Session.GetString("_Name") != null)
            {
                LoggedUser = HttpContext.Session.GetString("_Name");
            }
            if (name=="")
            {
                Blogs = await _blogprovider.GetBlogs();
            }
            else{
                var blog =await _blogprovider.GetBlog(name); //get 1 specific blog
                Blogs = new Blog[]{blog};
            }
            if (Blogs == null)
            {
                return NotFound();
            }
            return Page();
        }

    }

}