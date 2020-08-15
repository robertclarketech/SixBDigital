namespace SixBDigital.Domain.Entities
{
	using FluentValidation.Results;
	using SixBDigital.Domain.Entities.Abstract;
	using SixBDigital.Domain.Validators;

	public class User : BaseEntity
	{
		internal User()
		{
		}

		public string Username { get; internal set; } = string.Empty;
		public string Password { get; internal set; } = string.Empty;

		public override ValidationResult Validate()
		{
			return new UserValidator().Validate(this);
		}
	}
}
