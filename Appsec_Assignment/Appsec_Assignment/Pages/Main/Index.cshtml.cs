using System.Net.Mail;
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

        public async Task<IActionResult> OnGetReset()
        {

            //port? : https://localhost:44358/Account/Login
            string resetLink = "https://localhost:44358/Main/ResetPass";
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("irazachary@gmail.com");
            mail.To.Add(HttpContext.Session.GetString("_Email"));
            mail.Subject = ("Reset Password");
            mail.Body = ("Reset password link: " + resetLink);
            //Console.WriteLine("OPT Passed:" + OTPNo);
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            //smtp.Timeout = 1000;
            string pass = "iadqahickdoimyqd";
            smtp.Credentials = new System.Net.NetworkCredential("irazachary@gmail.com", pass);
            smtp.Send(mail);


            TempData["FlashMessage.Type"] = "success";
            TempData["FlashMessage.Text"] = "Email has been sent!";
            return Redirect("/Main/Index");
        }

    }
}
