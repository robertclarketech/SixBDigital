using SixBDigital.Domain.Entities;

namespace SixBDigital.Domain.Validators
{
	using FluentValidation;

	public class UserValidator : AbstractValidator<User>
	{
		public UserValidator()
		{
			RuleFor(e => e.Username).NotEmpty();
			RuleFor(e => e.Password).NotEmpty();
		}
	}
}
