namespace SixBDigital.Domain.Builders
{
	using System;
	using SixBDigital.Domain.Builders.Interfaces;
	using SixBDigital.Domain.Commands;
	using SixBDigital.Domain.Entities;

	public class BookingBuilder : IBuilder<CreateBookingCommand, Booking>
	{
		public Booking Build(CreateBookingCommand command)
		{
			if (command == null)
			{
				throw new ArgumentNullException(nameof(command));
			}

			return new Booking
			{
				BookingDate = command.BookingDate,
				ContactNumber = command.ContactNumber,
				EmailAddress = command.EmailAddress,
				Flexibility = command.Flexibility,
				Name = command.Name,
				VehicleSize = command.VehicleSize
			}.Validate();
		}
	}
}
