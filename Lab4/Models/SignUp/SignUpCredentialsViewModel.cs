using System;
using System.ComponentModel.DataAnnotations;
namespace Backend4.Models.SignUp
{
	public class SignUpCredentialsViewModel: SignUpViewModel
	{
		[Required(ErrorMessage = "Email address is required")]
		[EmailAddress(ErrorMessage = "Email address is invalid")]
		public String Email { get; set; }

		[Required(ErrorMessage = "Password is required")]
		public String Password { get; set; }

		[Compare("Password", ErrorMessage = "Confirm Password != Password")]
		public String ConfirmPassword { get; set; }

		public Boolean Remember { get; set; }

		public Boolean Existed { get; set; }

		public SignUpCredentialsViewModel() { }

		public SignUpCredentialsViewModel(SignUpViewModel parent, Boolean existed) : base(parent)
		{
			this.Existed = existed;
		}
	}
}
