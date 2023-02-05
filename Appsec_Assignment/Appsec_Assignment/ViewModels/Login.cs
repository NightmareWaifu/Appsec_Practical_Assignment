using System.ComponentModel.DataAnnotations;

namespace Appsec_Assignment.ViewModels
{
	public class Login
	{
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required]
		public string Token { get; set; }
	}
}
