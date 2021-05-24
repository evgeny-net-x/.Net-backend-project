using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend4.Services;
using Backend4.Models.SignUp;
using Microsoft.AspNetCore.Mvc;

namespace Backend4.Controllers
{
	public class SignUpController: Controller
	{
		private readonly IUserRegistrationService userRegistrationService;

		public SignUpController(IUserRegistrationService userRegistrationService)
		{
			this.userRegistrationService = userRegistrationService;
		}

		public IActionResult SignUp()
		{
			var model = new SignUpViewModel();
			return this.View(model);
		}

		[HttpPost]
		public IActionResult SignUp(SignUpViewModel model)
		{
			if (!this.ModelState.IsValid)
				return this.View(model);

			if (userRegistrationService.HasUser(model.FirstName, model.LastName, model.BirthdayDay, model.BirthdayMonth, model.BirthdayYear, model.Gender))
				return this.View("SignUpAlreadyExists", model);

			return this.View("SignUpCredentials", new SignUpCredentialsViewModel(model, false));
		}


		public IActionResult SignUpAnyway()
		{
			return this.View();
		}

		[HttpPost]
		public IActionResult SignUpAnyway(SignUpViewModel model)
		{
			if (!this.ModelState.IsValid)
				return this.View(model);

			//return this.View("SignUpCredentials", new SignUpCredentialsViewModel(model, true));
			//return this.RedirectToAction("SignUpCredentials", new SignUpCredentialsViewModel(model, true));
		}

		public IActionResult SignUpCredentials()
		{
			return this.View();
		}

		[HttpPost]
		public IActionResult SignUpCredentials(SignUpCredentialsViewModel model)
		{
			if (!this.ModelState.IsValid)
				return this.View(model);

			userRegistrationService.RegisterUser(model.FirstName, model.LastName, model.BirthdayDay, model.BirthdayMonth, model.BirthdayYear, model.Gender, model.Email, model.Password);
			return this.View("SignUpResult", model);
		}


		public IActionResult SignUpResult()
		{
			return this.View();
		}
	}
}
