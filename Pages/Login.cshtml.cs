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
        private readonly IUserProvider _provider;
        public LoginModel(IUserProvider Provider)
        {
            _provider = Provider;
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
        public User User_Attempt {get;set;}
        public string Error {get;set;}

        public async Task<IActionResult> OnPostAsync()
        {                                               // TRY CATCH FOR DUPLICATE NAMES
                var account = await _provider.GetUser(User_Attempt.Name);
                if (account == null || !BC.Verify(User_Attempt.PasswordHash, account.PasswordHash))
                {
                    Error = $"Wrong credentials";
                    return Page();
                }
                else
                {
                    HttpContext.Session.SetInt32("_Id", User_Attempt.Id);
                    HttpContext.Session.SetString("_Name", User_Attempt.Name);
                    return RedirectToPage("./Index");
                }

        }
    }

}
