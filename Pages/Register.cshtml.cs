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
    public class RegisterModel : PageModel
    {
        private readonly IBlogValidator _blogvalidator;
        private readonly IBlogProvider _blogprovider;
        public RegisterModel(IBlogValidator Validator, IBlogProvider Provider)
        {
            _blogvalidator = Validator;
            _blogprovider = Provider;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        [BindProperty]
        public Blog New_User {get; set; }
        public string Error {get;set;}

        public async Task<IActionResult> OnPostAsync()
        {
            string[] error_array = _blogvalidator.IsValid(New_User);
            if (error_array.Length!=0)
            {
                Error = error_array[0];
                return Page();
            }
            New_User.PasswordHash = BC.HashPassword(New_User.PasswordHash);
            try
            {                                            // TRY CATCH FOR DUPLICATE NAMES
                await _blogprovider.AddBlog(New_User);
                HttpContext.Session.SetInt32("_Id", New_User.UserId);
                HttpContext.Session.SetString("_Name", New_User.UserName);
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
