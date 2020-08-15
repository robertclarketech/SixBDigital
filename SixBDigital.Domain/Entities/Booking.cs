namespace SixBDigital.Domain.Entities
{
	using System;
	using FluentValidation;
	using FluentValidation.Results;
	using SixBDigital.Domain.Entities.Abstract;
	using SixBDigital.Domain.Enums;
	using SixBDigital.Domain.Validators;

	public class Booking : BaseEntity
	{
		internal Booking() { }
		public string Name { get; internal set; } = string.Empty;
		public DateTime BookingDate { get; internal set; }
		public Flexibility Flexibility { get; internal set; }
		public VehicleSize VehicleSize { get; internal set; }
		public string ContactNumber { get; internal set; } = string.Empty;
		public string EmailAddress { get; internal set; } = string.Empty;

		public ValidationResult Validate()
		{
			return new BookingValidator().Validate(this);
		}
	}
}
