using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using BC = BCrypt.Net.BCrypt;

namespace Blogg.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IBlogProvider _blogprovider;
        public LoginModel(IBlogProvider Provider)
        {
            _blogprovider = Provider;
        }
        public IActionResult OnGet()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("_Name")))
            {
                return RedirectToPage("./Index");
            }
            else
            {
                return Page();
            }
        }
        [BindProperty]
        public Blog User_Attempt {get;set;}
        public string Error {get;set;}
        public async Task<IActionResult> OnPostAsync()
        {
                var account = await _blogprovider.GetBlog(User_Attempt.UserName);
                if (account == null || !BC.Verify(User_Attempt.PasswordHash, account.PasswordHash))
                {
                    Error = $"Wrong credentials";
                    return Page();
                }
                else
                {
                    HttpContext.Session.SetString("_Name", User_Attempt.UserName);
                    return RedirectToPage("./Index");
                }

        }
    }

}
