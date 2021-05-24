using System;

namespace Backend4.Services
{
	public enum Gender
	{
		Male,
		Female
	};

	public interface IUserRegistrationService
	{
		void RegisterUser(String firstName, String lastName, Byte birthdayDay, Byte birthdayMonth, Int16 BirthdayYear, Gender gender, String email, String password);
		Boolean HasUser(String firstName, String lastName, Byte birthdayDay, Byte birthdayMonth, Int16 BirthdayYear, Gender gender);
	}
}
