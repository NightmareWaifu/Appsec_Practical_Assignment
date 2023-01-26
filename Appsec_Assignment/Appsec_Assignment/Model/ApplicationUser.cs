using System.ComponentModel;
using Microsoft.AspNetCore.Identity;

namespace Appsec_Assignment.Model
{
	public class ApplicationUser : IdentityUser
	{
		public string FullName { get; set; }
		public string CreditCard { get; set; }
		public string Gender { get; set; }
		//public string PhoneNo { get; set; }
		public string Address { get; set; }
		public string AboutMe { get; set; }
		public string ImagePath { get; set; }


		[DefaultValue(false)]
		public bool loggedIn { get; set; }

		public int attempts { get; set; }

	}
}
