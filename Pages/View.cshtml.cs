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
        private readonly IBlogProvider _blogprovider;
        public Blog Author;
        public string LoggedUser;
        [BindProperty]
        public ICollection<Post> Posts {get; set; }

        public ViewModel(IBlogProvider Provider)
        {
            _blogprovider = Provider;
        }

        public async Task<IActionResult> OnGetAsync(string name)
        {
            if (HttpContext.Session.GetString("_Name") != null)
            {
                LoggedUser = HttpContext.Session.GetString("_Name");
            }
            if (name == null)
            {
                return NotFound();
            }

            Author = await _blogprovider.GetBlog(name);
            Posts=Author.Posts;
            Console.WriteLine(Author.Posts);
            if (Posts == null)
            {
                return NotFound();
            }
            return Page();
        }

    }

}
