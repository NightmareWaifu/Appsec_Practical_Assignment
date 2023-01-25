using Appsec_Assignment.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Appsec_Assignment.ViewModels;

namespace Appsec_Assignment.Pages.Account
{        
    

   
    public class OTPModel : PageModel
    {
        private SignInManager<ApplicationUser> _signInManager;

        private UserManager<ApplicationUser> _userManager;

        public OTPModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [BindProperty]

        public string OTP { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            var user = await _userManager.FindByEmailAsync(HttpContext.Session.GetString("_Email"));
            if(user == null)
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = "Session timed out.";
                return RedirectToPage("Login");
            }

            if(OTP == HttpContext.Session.GetString("_otp"))
            {
                await _signInManager.SignInAsync(user, true); //authenticates the user
                //user.loggedIn = true;
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = "Logged in successfully!";
                return Redirect("../Main/Index");
            }
            else
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = "OTP is incorrect.";
                return Page();
            }
            


        }
    }
}
