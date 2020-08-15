namespace SixBDigital.Domain.Builders
{
	using System;
	using FluentValidation;
	using SixBDigital.Domain.Entities;

	public class UserBuilder
	{
		private readonly User _user;

		public UserBuilder()
		{
			_user = new User();
		}

		public UserBuilder SetUsername(string username)
		{
			_user.Username = username ?? throw new ArgumentNullException(nameof(username));
			return this;
		}

		public UserBuilder SetPassword(string password)
		{
			_user.Username = password ?? throw new ArgumentNullException(nameof(password));
			return this;
		}

		public User Build()
		{
			var result = _user.Validate();
			return result.IsValid ? _user : throw new ValidationException("User was invalid", result.Errors);
		}
	}
}