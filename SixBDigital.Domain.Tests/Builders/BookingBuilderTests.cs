namespace SixBDigital.Domain.Tests.Builders
{
	using System;
	using FluentValidation;
	using SixBDigital.Domain.Builders;
	using SixBDigital.Domain.Commands;
	using SixBDigital.Domain.Enums;
	using Xunit;

	public class BookingBuilderTests
	{
		[Fact]
		public void Build_WithANullCommand_Throws()
		{
			//arrange
			var builder = new BookingBuilder();

			//act + assert
			Assert.Throws<ArgumentNullException>(() => builder.Build(null));
		}

		[Fact]
		public void Build_WithAnInvalidCommand_Throws()
		{
			//arrange
			var builder = new BookingBuilder();
			var command = new CreateBookingCommand();

			//act + assert
			Assert.ThrowsAny<Exception>(() => builder.Build(command));
		}

		[Fact]
		public void Build_WithAValidCommand_ReturnsBooking()
		{
			//arrange
			var validCommand = new CreateBookingCommand
			{
				BookingDate = DateTime.Now,
				ContactNumber = "0123456789",
				EmailAddress = "email@address.com",
				Flexibility = Flexibility.PlusMinusOneDay,
				Name = "Name",
				VehicleSize = VehicleSize.Large,
			};
			var builder = new BookingBuilder();

			//act
			var booking = builder.Build(validCommand);

			//assert
			Assert.Equal(validCommand.BookingDate, booking.BookingDate);
			Assert.Equal(validCommand.ContactNumber, booking.ContactNumber);
			Assert.Equal(validCommand.EmailAddress, booking.EmailAddress);
			Assert.Equal(validCommand.Flexibility, booking.Flexibility);
			Assert.Equal(validCommand.Name, booking.Name);
			Assert.Equal(validCommand.VehicleSize, booking.VehicleSize);
		}
	}
}
