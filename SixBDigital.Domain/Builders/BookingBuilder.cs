namespace SixBDigital.Domain.Builders
{
	using System;
	using FluentValidation;
	using SixBDigital.Domain.Entities;
	using SixBDigital.Domain.Enums;

	public class BookingBuilder
	{
		private readonly Booking _booking;
		public BookingBuilder()
		{
			_booking = new Booking();
		}

		public BookingBuilder SetName(string name)
		{
			_booking.Name = name ?? throw new ArgumentNullException(nameof(name));
			return this;
		}

		public BookingBuilder SetBookingDate(DateTime bookingDate)
		{
			_booking.BookingDate = bookingDate;
			return this;
		}
		public BookingBuilder SetFlexibility(Flexibility flexibility)
		{
			_booking.Flexibility = flexibility;
			return this;
		}
		public BookingBuilder SetVehicleSize(VehicleSize vehicleSize)
		{
			_booking.VehicleSize = vehicleSize;
			return this;
		}
		public BookingBuilder SetContactNumber(string contactNumber)
		{
			_booking.ContactNumber = contactNumber ?? throw new ArgumentNullException(nameof(contactNumber));
			return this;
		}

		public BookingBuilder SetEmailAddress(string emailAddress)
		{
			_booking.EmailAddress = emailAddress ?? throw new ArgumentNullException(nameof(emailAddress));
			return this;
		}

		public Booking Build()
		{
			var result = _booking.Validate();
			return result.IsValid ? _booking : throw new ValidationException("Booking was invalid", result.Errors);
		}
	}
}
