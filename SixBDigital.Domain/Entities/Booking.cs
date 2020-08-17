namespace SixBDigital.Domain.Entities
{
	using System;
	using FluentValidation;
	using FluentValidation.Results;
	using SixBDigital.Domain.Commands;
	using SixBDigital.Domain.Entities.Abstract;
	using SixBDigital.Domain.Enums;
	using SixBDigital.Domain.Validators;

	public class Booking : BaseEntity<Booking>
	{
		protected internal Booking()
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
			var backup = Name;
			Name = name ?? throw new ArgumentNullException(nameof(name));
			return Validate(e => e.Name = backup);
		}

		public Booking UpdateBookingDate(DateTime date)
		{
			var backup = BookingDate;
			BookingDate = date;
			return Validate(e => e.BookingDate = backup);
		}

		public Booking UpdateFlexibility(Flexibility flexibility)
		{
			var backup = Flexibility;
			Flexibility = flexibility;
			return Validate(e => e.Flexibility = backup);
		}

		public Booking UpdateVehicleSize(VehicleSize vehicleSize)
		{
			var backup = VehicleSize;
			VehicleSize = vehicleSize;
			return Validate(e => e.VehicleSize = backup);
		}

		public Booking UpdateContactNumber(string contactNumber)
		{
			var backup = ContactNumber;
			ContactNumber = contactNumber ?? throw new ArgumentNullException(nameof(contactNumber));
			return Validate(e => e.ContactNumber = backup);
		}

		public Booking UpdateEmailAddress(string emailAddress)
		{
			var backup = EmailAddress;
			EmailAddress = emailAddress ?? throw new ArgumentNullException(nameof(emailAddress));
			return Validate(e => e.EmailAddress = backup);
		}

		public Booking UpdateApproved(bool approved)
		{
			var backup = Approved;
			Approved = approved;
			return Validate(e => e.Approved = backup);
		}

		internal override Booking Validate(Action<Booking>? onFail = null)
		{
			var validationResults = new BookingValidator().Validate(this);
			if (!validationResults.IsValid)
			{
				onFail?.Invoke(this);
				throw new ValidationException("Booking is not valid", validationResults.Errors);
			}
			return this;
		}
	}
}
