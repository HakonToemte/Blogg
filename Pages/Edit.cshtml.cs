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
    public class EditModel : PageModel
    {
        private readonly IPostValidator _postvalidator;
        private readonly IPostProvider _postprovider;
        private readonly IBlogProvider _blogprovider;
        public Blog LoggedUser;
        

        public EditModel(IPostValidator Validator, IPostProvider Provider, IBlogProvider BlogProvider)
        {
            _postvalidator = Validator;
            _postprovider = Provider;
            _blogprovider = BlogProvider;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetString("_Name") != null)
            {
                LoggedUser = await _blogprovider.GetBlog(HttpContext.Session.GetString("_Name"));
            }
            else{
                return RedirectToPage("./Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            Post = await _postprovider.GetPost(id.Value);

            if (Post == null)
            {
                return NotFound();
            }
            return Page();
        }
        [BindProperty]
        public Post Post {get; set; }
        public string Error{get;set;}

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var postToUpdate = await _postprovider.GetPost(id);
            if (postToUpdate == null)
            {
                return NotFound();
            }
            if (postToUpdate.Blog.UserName != HttpContext.Session.GetString("_Name")){ // If person logged in is not the author of the post.
                Error = "You are not the author of this post";                         // Dont allow them to edit.
                return Page();
            }
            if (await TryUpdateModelAsync<Post>(
                postToUpdate,
                "post",
                s => s.Title, s => s.Text))
            {
                string[] validarray = _postvalidator.IsValid(postToUpdate);
                if (validarray.Length!= 0)
                {
                    Error= validarray[0];
                    return Page();
                }
                try
                {                                            //FOR DUPLICATE NAMES
                    await _postprovider.UpdatePost(id, postToUpdate);
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
            return Page();
        }
    }

}
