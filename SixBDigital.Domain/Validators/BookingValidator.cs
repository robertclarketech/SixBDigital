namespace SixBDigital.Domain.Validators
{
	using SixBDigital.Domain.Entities;
	using FluentValidation;

	public class BookingValidator : AbstractValidator<Booking>
	{
		public BookingValidator()
		{
			RuleFor(x => x.Name).NotEmpty();
			RuleFor(x => x.BookingDate).NotEmpty();
			RuleFor(x => x.Flexibility).NotNull();
			RuleFor(x => x.VehicleSize).NotNull();
			RuleFor(x => x.ContactNumber).NotEmpty();
			RuleFor(x => x.EmailAddress).NotEmpty();
		}
	}
}
