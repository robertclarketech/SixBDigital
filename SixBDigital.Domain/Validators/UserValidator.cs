namespace SixBDigital.Domain.Validators
{
	using SixBDigital.Domain.Entities;
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
