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
        private readonly IBlogPostValidator _validator;
        private readonly IBlogPostProvider _provider;
        private readonly IUserProvider _userprovider;
        public User LoggedUser;
        

        public EditModel(IBlogPostValidator Validator, IBlogPostProvider Provider, IUserProvider UserProvider)
        {
            _validator = Validator;
            _provider = Provider;
            _userprovider = UserProvider;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetString("_Name") != null)
            {
                LoggedUser = await _userprovider.GetUser(HttpContext.Session.GetString("_Name"));
            }
            else{
                return RedirectToPage("./Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            Post = await _provider.GetBlogPost(id.Value);

            if (Post == null)
            {
                return NotFound();
            }
            return Page();
        }
        [BindProperty]
        public BlogPost Post {get; set; }
        public string Error{get;set;}

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var postToUpdate = await _provider.GetBlogPost(id);
            if (postToUpdate == null)
            {
                return NotFound();
            }
            if (await TryUpdateModelAsync<BlogPost>(
                postToUpdate,
                "post",
                s => s.Title, s => s.Text))
            {
                string[] validarray = _validator.IsValid(postToUpdate);
                if (validarray.Length!= 0)
                {
                    Error= validarray[0];
                    return Page();
                }
                try
                {                                            //FOR DUPLICATE NAMES
                    await _provider.UpdateBlogPost(id, postToUpdate);
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
