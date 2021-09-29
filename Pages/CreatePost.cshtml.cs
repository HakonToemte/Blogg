using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Blogg.Pages
{
    public class CreatePostModel : PageModel
    {
        private readonly IPostValidator _postvalidator;
        private readonly IPostProvider _postprovider;
        private readonly IBlogProvider _blogprovider;
        public CreatePostModel(IPostValidator Validator, IPostProvider Provider, IBlogProvider BlogProvider)
        {
            _postvalidator = Validator;
            _postprovider = Provider;
            _blogprovider =BlogProvider;
        }

        public async Task<IActionResult> OnGet()
        {
            if (HttpContext.Session.GetString("_Name") != null)
            {
                LoggedUser = await _blogprovider.GetBlog(HttpContext.Session.GetString("_Name"));
            }
            return Page();
        }
        [BindProperty]
        public Post Post {get; set; }
        public Blog LoggedUser{get; set;}
        public string Error {get;set;}
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (HttpContext.Session.GetString("_Name") != null)
            {
                LoggedUser = await _blogprovider.GetBlog(HttpContext.Session.GetString("_Name"));
            }
            else{
                return RedirectToPage("./Index");
            }
            Post.Blog = LoggedUser;
            string[] error_array = _postvalidator.IsValid(Post);
            if (error_array.Length!=0)
            {
                Error = error_array[0];
                return Page();
            }
            try
            {                                            // TRY CATCH FOR DUPLICATE NAMES
                await _postprovider.AddPost(Post);
                return RedirectToPage("./Index");
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
