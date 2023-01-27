using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Appsec_Assignment.Model;
using Appsec_Assignment.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.SqlServer.Server;
using static System.Net.Mime.MediaTypeNames;

namespace Appsec_Assignment.Pages
{
    public class RegisterModel : PageModel
    {

        private UserManager<ApplicationUser> userManager { get; }
        private SignInManager<ApplicationUser> signInManager { get; }
        private IWebHostEnvironment _webHostEnvironment;

        //public ApplicationUser user;

        [BindProperty]
        public Register RModel { get; set; }
        public IFormFile? image { get; set; }
        public RegisterModel(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IWebHostEnvironment webHostEnvironment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
        }



        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if(image == null)
                {
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = "Image is required";
                    return Page();
                }
                
                if (Path.GetExtension(image.FileName) != ".JPG" && Path.GetExtension(image.FileName) != ".jpg")
                {
                    //TempData["FlashMessage.Type"] = "danger";
                    ModelState.AddModelError("Image has to be in .jpg format", "Image has to be in .jpg format");
                    //TempData["FlashMessage.Text"] = "Image has to be .jpg | " + Path.GetExtension(image.FileName);
                    return Page();
                }
                //check password complexity
                //image
                var imagesFolder = "uploads";
                var imageFile = Guid.NewGuid() + Path.GetExtension(image.FileName);
                var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", imagesFolder, imageFile);
                using var fileStream = new FileStream(imagePath, FileMode.Create);
                image.CopyTo(fileStream);
                RModel.ImagePath = String.Format("/" + imagesFolder + "/" + imageFile);

                var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
                var protector = dataProtectionProvider.CreateProtector("MySecretKey");

                var user = new ApplicationUser()
                {
                    UserName = RModel.Email,
                    Email = RModel.Email,
                    FullName = RModel.FullName,
                    CreditCard = protector.Protect(RModel.CreditCard),

                    ImagePath = RModel.ImagePath,

                    Gender = RModel.Gender,
                    PhoneNumber = RModel.PhoneNo,
                    Address = RModel.Address,
                    AboutMe = RModel.AboutMe
                };
                var result = await userManager.CreateAsync(user, RModel.Password);
                if (result.Succeeded)
                {
                    //await signInManager.SignInAsync(user, false);
                    TempData["FlashMessage.Type"] = "success";
                    TempData["FlashMessage.Text"] = RModel.FullName + " added successfully!";
                    return RedirectToPage("Login");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Page();
        }







    }
}
