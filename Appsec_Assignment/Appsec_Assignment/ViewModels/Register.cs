using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Appsec_Assignment.ViewModels
{
    public class Register
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        [DataType(DataType.CreditCard)]
        [RegularExpression(@"^[0-9]{12}$", ErrorMessage = "Incorrect card format - Must contain 12 numerical digits only")]
        public string CreditCard { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Key]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        //[RegularExpression(@"^(?=.[A-Z])(?=.[!@#$%^&])(?=.[0-9])(?=.*[a-z]).{12,}$")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        
        [Compare(nameof(Password), ErrorMessage = "Password and confirm password does not match")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9]{8}$", ErrorMessage = "Invalid Phone Number Format - Must contain 8 numerical digits only")]
        public string PhoneNo { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string AboutMe { get; set; }
        
        public string? ImagePath { get; set; }




    }
}
