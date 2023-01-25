using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Appsec_Assignment.ViewModels
{
    public class Register
    {
        public string FullName { get; set; }
        public string CreditCard { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string PhoneNo { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string AboutMe { get; set; }
        
        public string? ImagePath { get; set; }




    }
}
