namespace SixBDigital.Infrastructure.Tests.CommandHandlers
{
	using System;
	using System.Threading.Tasks;
	using MediatR;
	using Microsoft.EntityFrameworkCore;
	using SixBDigital.Domain.Commands;
	using SixBDigital.Domain.Entities;
	using SixBDigital.Domain.Enums;
	using SixBDigital.Domain.Exceptions;
	using SixBDigital.Infrastructure.CommandHandlers;
	using SixBDigital.Infrastructure.Tests.Abstracts;
	using Xunit;

	public class EditBookingCommandHandlerTests : InMemoryDbContextBase
	{
		private readonly IRequestHandler<EditBookingCommand> _handler;

		public EditBookingCommandHandlerTests()
		{
			_handler = new EditBookingCommandHandler(Context);
		}

		[Fact]
		public async Task Handle_WithAnInvalidEditBookingCommand_Throws()
		{
			//arrange
			var invalidCommand = new EditBookingCommand { Id = 0 };

			//act + assert
			_ = await Assert
				.ThrowsAsync<BookingNotFoundException>(() => _handler.Handle(invalidCommand, default))
				.ConfigureAwait(false);
		}

		[Fact]
		public async Task Handle_WithAValidEditBookingCommand_UpdatesBooking()
		{
			//arrange
			Context.Add(new Booking
			{
				BookingDate = DateTime.Now,
				ContactNumber = "0123456789",
				EmailAddress = "email@address.com",
				Flexibility = Flexibility.PlusMinusOneDay,
				Name = "Name",
				VehicleSize = VehicleSize.Large,
				Approved = false
			});
			_ = await Context
				.SaveChangesAsync()
				.ConfigureAwait(false);

			var validCommand = new EditBookingCommand
			{
				Id = 1,
				BookingDate = DateTime.Now.AddMonths(1),
				ContactNumber = "9876543210",
				EmailAddress = "new@email.com",
				Flexibility = Flexibility.PlusMinusThreeDays,
				Name = "NewName",
				VehicleSize = VehicleSize.Medium,
				Approved = true
			};

			//act
			_ = await _handler
				.Handle(validCommand, default)
				.ConfigureAwait(false);

			//assert
			var actual = await Context
				.Bookings
				.SingleAsync()
				.ConfigureAwait(false);
			Assert.Equal(validCommand.BookingDate, actual.BookingDate);
			Assert.Equal(validCommand.ContactNumber, actual.ContactNumber);
			Assert.Equal(validCommand.EmailAddress, actual.EmailAddress);
			Assert.Equal(validCommand.Flexibility, actual.Flexibility);
			Assert.Equal(validCommand.Name, actual.Name);
			Assert.Equal(validCommand.VehicleSize, actual.VehicleSize);
			Assert.Equal(validCommand.Approved, actual.Approved);
		}
	}
}
