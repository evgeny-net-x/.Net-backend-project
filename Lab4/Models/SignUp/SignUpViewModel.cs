using System;
using Backend4.Services;
using System.ComponentModel.DataAnnotations;

namespace Backend4.Models.SignUp
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage="First name is requiared")]
		[RegularExpression(@"[a-zA-Z]+", ErrorMessage="First name contains only letters")]
		public String FirstName { get; set; }

		[Required(ErrorMessage = "Last name is requiared")]
		[RegularExpression(@"[a-zA-Z]+", ErrorMessage = "Last name contains only letters")]
		public String  LastName { get; set; }

		[Required(ErrorMessage = "Day of birthday is requiared")]
		[Range(1, 31, ErrorMessage = "Day range is 1-31")]
		public Byte BirthdayDay { get; set; }

		[Required(ErrorMessage = "Month of birthday is requiared")]
		[Range(1, 12, ErrorMessage = "Month range is 1-12")]
		public Byte BirthdayMonth { get; set; }

		[Required(ErrorMessage = "Year of birthday is requiared")]
		[Range(1921, 2021, ErrorMessage = "Year range is 1921-2021")]
		public Int16 BirthdayYear { get; set; }

		[Required(ErrorMessage = "Gender is requiared")]
		public Gender Gender { get; set; }

		public SignUpViewModel() { }

		public SignUpViewModel(SignUpViewModel model)
		{
			this.FirstName = model.FirstName;
			this.LastName = model.LastName;
			this.BirthdayDay = model.BirthdayDay;
			this.BirthdayMonth = model.BirthdayMonth;
			this.BirthdayYear = model.BirthdayYear;
			this.Gender = model.Gender;
		}
	}
}
