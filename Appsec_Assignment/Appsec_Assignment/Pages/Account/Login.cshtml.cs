using System.Net.Mail;
using Appsec_Assignment.Model;
using Appsec_Assignment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Appsec_Assignment.Pages
{
    public class LoginModel : PageModel
    {
        private readonly GoogleCaptchaService _googleCaptchaService;
        private UserManager<ApplicationUser> userManager { get; }

        private SignInManager<ApplicationUser> signInManager { get; }
        public string OTPNo;
        public string startDate;

        //private SignInManager<ApplicationUser> signInManager { get; }

        //private readonly IHttpContextAccessor sessionData;

        public const string SessionEmail = "_Email";

        public LoginModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, GoogleCaptchaService googleCaptchaService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this._googleCaptchaService = googleCaptchaService;
        }

        [BindProperty]
        public Login loginModel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var captchaResult = await _googleCaptchaService.VerifyToken(loginModel.Token);
            if(!captchaResult)
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = "Captcha failed.";
                return Page();
            }


            if(ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(loginModel.Email);
                if (user == null)
                {
                    ModelState.AddModelError("message", "Email or Password is incorrect!");
                    return Page();
                }
                if (await userManager.CheckPasswordAsync(user,loginModel.Password) == false)
                {
                    ModelState.AddModelError("message", "Email or Password is incorrect!");
                    return Page();
                }

                if(User.Identity.IsAuthenticated)
                {
                    //user is already in another session
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = "Already logged in on another device!";
                    return Page();
                }
                //await signInManager.SignInAsync(user, true); //authenticates the user
                //add session data here if you need any
                HttpContext.Session.SetString(SessionEmail, loginModel.Email);
                
                

                //SMTP Logic
                try
                {
                    OTPNo = string.Empty;
                    Random rnd = new Random();
                    OTPNo = (rnd.Next(100000, 999999)).ToString();
                    HttpContext.Session.SetString("_otp", OTPNo);
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("irazachary@gmail.com");
                    mail.To.Add(HttpContext.Session.GetString("_Email"));
                    mail.Subject = ("Your OTP number.");
                    mail.Body = ("Your OTP is " + OTPNo);
                    Console.WriteLine("OPT Passed:" + OTPNo);
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.EnableSsl = true;
                    //smtp.Timeout = 1000;
                    string pass = "iadqahickdoimyqd";
                    smtp.Credentials = new System.Net.NetworkCredential("irazachary@gmail.com", pass);
                    smtp.Send(mail);


                    TempData["FlashMessage.Type"] = "success";
                    TempData["FlashMessage.Text"] = "OTP sent!";
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed reason" + ex.ToString());
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = "Failed to send OTP.";
                }
                return RedirectToPage("OTP");
            }
            return Page();
        }

    }
}
