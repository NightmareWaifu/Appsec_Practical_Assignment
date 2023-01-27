using Appsec_Assignment.Model;
using Appsec_Assignment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Appsec_Assignment.Pages.Main
{
    [Authorize]
    public class IndexModel : PageModel
    {

        private SignInManager<ApplicationUser> _signInManager { get; set; }
        private UserManager<ApplicationUser> _userManager { get; }
        //public string timedOutEmail { get; set; }

        [BindProperty]

        public ApplicationUser CurrentUser { get; set; }

        public IndexModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGet()
        {
            //CurrentUser = await _userManager.FindByEmailAsync(HttpContext.Session.GetString("_Email"));

            if (HttpContext.Session.GetString("_Email") == null)
            {
                await _signInManager.SignOutAsync();
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = "Session timed out.";
                return Redirect("../Account/Login");
            }
            CurrentUser = await _userManager.FindByEmailAsync(HttpContext.Session.GetString("_Email"));
            return Page();
        }

        public async Task<IActionResult> OnGetLogout()
        {
            await _signInManager.SignOutAsync();
            //CurrentUser.loggedIn = false;
            HttpContext.Session.Clear();
            TempData["FlashMessage.Type"] = "success";
            TempData["FlashMessage.Text"] = "Logged out!";
            return Redirect("../Account/Login");
        }

    }
}
