using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Backend4.Services
{
	public class UserRegistrationService : IUserRegistrationService
	{
		private readonly ILogger logger;
		private readonly List<UserEntry> users = new List<UserEntry>();

		public UserRegistrationService(ILogger<IUserRegistrationService> logger)
		{
			this.logger = logger;
		}

		public void RegisterUser(String firstName, String lastName, Byte birthdayDay, Byte birthdayMonth, Int16 birthdayYear, Gender gender, String email, String password)
		{
			lock (this.users)
			{
				var user = this.FindUser(firstName, lastName, birthdayDay, birthdayMonth, birthdayYear, gender);
				if (user == null)
				{
					user = new UserEntry(firstName, lastName, birthdayDay, birthdayMonth, birthdayYear, gender, email, password);
					this.users.Add(user);
					this.logger.LogInformation($"Registaring new user {firstName} {lastName}");
				} else
				{
					user.Email = email;
					user.Password = password;
					this.logger.LogInformation($"Updating email={email} and password={password} of existed user");
				}
			}
		}

		public Boolean HasUser(String firstName, String lastName, Byte birthdayDay, Byte birthdayMonth, Int16 birthdayYear, Gender gender)
		{
			foreach (var user in this.users)
			{
				if (user.FirstName == firstName && user.LastName == lastName && user.BirthdayDay == birthdayDay && user.BirthdayMonth == birthdayMonth && user.BirthdayYear == birthdayYear && user.Gender == gender)
				{
					this.logger.LogInformation($"Searching user {firstName} {lastName}, {birthdayDay}.{birthdayMonth}.{birthdayYear}, {gender}...success");
					return true;
				}
			}

			this.logger.LogInformation($"Searching user {firstName} {lastName}, {birthdayDay}.{birthdayMonth}.{birthdayYear}, {gender}...failure");
			return false;
		}

		private UserEntry FindUser(String firstName, String lastName, Byte birthdayDay, Byte birthdayMonth, Int16 birthdayYear, Gender gender)
		{
			foreach (var user in this.users)
			{
				if (user.FirstName == firstName && user.LastName == lastName && user.BirthdayDay == birthdayDay && user.BirthdayMonth == birthdayMonth && user.BirthdayYear == birthdayYear && user.Gender == gender)
					return user;
			}

			return null;
		}

		private class UserEntry
		{
			public String FirstName { get; }
			public String  LastName { get; }

			public Byte BirthdayDay { get; }
			public Byte BirthdayMonth { get; }
			public Int16 BirthdayYear { get; }

			public Gender Gender { get; }
			public String Email { get; set; }
			public String Password { get; set; }

			public UserEntry(String firstName, String lastName, Byte birthdayDay, Byte birthdayMonth, Int16 birthdayYear, Gender gender, String email, String password)
			{
				this.FirstName = firstName;
				this.LastName = lastName;

				this.BirthdayDay = birthdayDay;
				this.BirthdayMonth = birthdayMonth;
				this.BirthdayYear = birthdayYear;

				this.Gender = gender;
				this.Email = email;
				this.Password = password;
			}
		};
	}
}
