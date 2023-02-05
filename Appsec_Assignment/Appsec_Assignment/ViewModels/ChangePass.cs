using System.ComponentModel.DataAnnotations;

namespace Appsec_Assignment.ViewModels
{
	public class ChangePass
	{
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string ChangePassword { get; set; }
    }
}
