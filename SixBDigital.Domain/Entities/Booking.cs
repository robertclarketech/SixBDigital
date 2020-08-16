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
		internal Booking()
		{
		}

		public string Name { get; internal set; } = string.Empty;
		public DateTime BookingDate { get; internal set; }
		public Flexibility Flexibility { get; internal set; }
		public VehicleSize VehicleSize { get; internal set; }
		public string ContactNumber { get; internal set; } = string.Empty;
		public string EmailAddress { get; internal set; } = string.Empty;
		public bool Approved { get; internal set; }

		public Booking UpdateName(string name)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			ValidateOnEdit();
			return this;
		}

		public Booking UpdateBookingDate(DateTime date)
		{
			BookingDate = date;
			ValidateOnEdit();
			return this;
		}

		public Booking UpdateFlexibility(Flexibility flexibility)
		{
			Flexibility = flexibility;
			ValidateOnEdit();
			return this;
		}

		public Booking UpdateVehicleSize(VehicleSize vehicleSize)
		{
			VehicleSize = vehicleSize;
			ValidateOnEdit();
			return this;
		}

		public Booking UpdateContactNumber(string contactNumber)
		{
			ContactNumber = contactNumber ?? throw new ArgumentNullException(nameof(contactNumber));
			ValidateOnEdit();
			return this;
		}

		public Booking UpdateEmailAddress(string emailAddress)
		{
			EmailAddress = emailAddress ?? throw new ArgumentNullException(nameof(emailAddress));
			ValidateOnEdit();
			return this;
		}

		public Booking UpdateApproved(bool approved)
		{
			Approved = approved;
			ValidateOnEdit();
			return this;
		}

		public override ValidationResult Validate()
		{
			return new BookingValidator().Validate(this);
		}

		private void ValidateOnEdit()
		{
			var validationResults = Validate();
			if (!Validate().IsValid)
			{
				throw new ValidationException("Booking is not valid", validationResults.Errors);
			}
		}
	}
}
