using System.ComponentModel.DataAnnotations;

namespace Appsec_Assignment.ViewModels
{
	public class Login
	{
		[Required]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		public string Token { get; set; }
	}
}
