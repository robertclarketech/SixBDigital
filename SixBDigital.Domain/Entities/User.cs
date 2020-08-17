namespace SixBDigital.Domain.Entities
{
	using System;
	using FluentValidation;
	using SixBDigital.Domain.Entities.Abstract;
	using SixBDigital.Domain.Validators;

	public class User : BaseEntity<User>
	{
		protected internal User()
		{
		}

		public string Username { get; internal set; } = string.Empty;
		public string Password { get; internal set; } = string.Empty;

		internal override User Validate(Action<User>? onFail = null)
		{
			var validationResults = new UserValidator().Validate(this);
			if (validationResults.IsValid)
			{
				onFail?.Invoke(this);
				throw new ValidationException("User is not valid", validationResults.Errors);
			}
			return this;
		}
	}
}
