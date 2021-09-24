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
    public class CreatePostModel : PageModel
    {
        private readonly IBlogPostValidator _validator;
        private readonly IBlogPostProvider _provider;
        private readonly IUserProvider _userprovider;
        public CreatePostModel(IBlogPostValidator Validator, IBlogPostProvider Provider, IUserProvider UserProvider)
        {
            _validator = Validator;
            _provider = Provider;
            _userprovider =UserProvider;
        }

        public async Task<IActionResult> OnGet()
        {
            if (HttpContext.Session.GetString("_Name") != null)
            {
                LoggedUser = await _userprovider.GetUser(HttpContext.Session.GetString("_Name"));
            }
            return Page();
        }
        [BindProperty]
        public BlogPost Post {get; set; }
        public User LoggedUser{get; set;}
        public string Error {get;set;}

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (HttpContext.Session.GetString("_Name") != null)
            {
                LoggedUser = await _userprovider.GetUser(HttpContext.Session.GetString("_Name"));
            }
            else{
                return RedirectToPage("./Admin");
            }
            Post.User = LoggedUser;
            string[] error_array = _validator.IsValid(Post);
            if (error_array.Length!=0)
            {
                Error = error_array[0];
                return Page();
            }
            try
            {                                            // TRY CATCH FOR DUPLICATE NAMES
                await _provider.AddBlogPost(Post);
                return RedirectToPage("./Admin");
            }catch (Exception duplicate_error)
            {
                if (duplicate_error.InnerException.Message.Contains("UNIQUE constraint failed")){
                    Error = "NAME IS ALREADY USED";
                    return Page();
                }
                else {
                    Error = "ERROR";
                    return Page();
                }
            }
        }
    }

}
