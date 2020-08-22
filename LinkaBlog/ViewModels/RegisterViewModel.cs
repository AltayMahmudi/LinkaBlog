using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LinkaBlog.ViewModels
{
	public class RegisterViewModel
	{
		[Required]
		public string UserName { get; set; }
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		//[Required]
		//public string UserName { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string password { get; set; }
		[Required]
		[DataType(DataType.Password)]
		[Compare("password")]
		public string confirmPassword { get; set; }

		[Required (ErrorMessage = "Please Check Privacy Policy")]
		public bool	PrivacyPolicy { get; set; }
		[Required(ErrorMessage = "Please  Check Terms and Conditions")]
		public bool TermsConditions { get; set; }
	}
}
