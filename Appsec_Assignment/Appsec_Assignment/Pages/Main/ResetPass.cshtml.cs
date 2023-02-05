using Appsec_Assignment.Model;
using Appsec_Assignment.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Query;

namespace Appsec_Assignment.Pages.Main
{
    public class ResetPassModel : PageModel
    {

        private readonly GoogleCaptchaService _googleCaptchaService;
        private UserManager<ApplicationUser> userManager { get; }

        private SignInManager<ApplicationUser> signInManager { get; }

        public ResetPassModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, GoogleCaptchaService googleCaptchaService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this._googleCaptchaService = googleCaptchaService;
        }

        [BindProperty]
        public ChangePass changePassModel { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (!(changePassModel.Email == HttpContext.Session.GetString("_Email")))
                {
                    ModelState.AddModelError("", "Email is incorrect!");
                    return Page();
                }
                var user = await userManager.FindByEmailAsync(changePassModel.Email);
                var changePasswordResult = await userManager.ChangePasswordAsync(user, changePassModel.CurrentPassword, changePassModel.ChangePassword);
                if (!changePasswordResult.Succeeded)
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return Page();
                }
                await signInManager.SignOutAsync();
                HttpContext.Session.Clear();
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = "Password Successfully changed! Please login again.";
                return Redirect("../Account/Login");
            }
            return Page();
        }
    }
}
