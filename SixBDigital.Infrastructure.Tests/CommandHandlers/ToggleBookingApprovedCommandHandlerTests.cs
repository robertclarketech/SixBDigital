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

	public class ToggleBookingApprovedCommandHandlerTests : InMemoryDbContextBase
	{
		private readonly IRequestHandler<ToggleBookingApprovedCommand> _handler;

		public ToggleBookingApprovedCommandHandlerTests()
		{
			_handler = new ToggleBookingApprovedCommandHandler(Context);
		}

		[Fact]
		public async Task Handle_WithAnInvalidEditBookingCommand_Throws()
		{
			//arrange
			var invalidCommand = new ToggleBookingApprovedCommand { Id = 0 };

			//act + assert
			_ = await Assert
				.ThrowsAsync<BookingNotFoundException>(() => _handler.Handle(invalidCommand, default))
				.ConfigureAwait(false);
		}

		[Fact]
		public async Task Handle_WithAValidEditBookingCommand_UpdatesBooking()
		{
			//arrange
			const bool expected = true;

			Context.Add(new Booking
			{
				BookingDate = DateTime.Now,
				ContactNumber = "0123456789",
				EmailAddress = "email@address.com",
				Flexibility = Flexibility.PlusMinusOneDay,
				Name = "Name",
				VehicleSize = VehicleSize.Large,
				Approved = !expected
			});
			_ = await Context
				.SaveChangesAsync()
				.ConfigureAwait(false);

			var validCommand = new ToggleBookingApprovedCommand
			{
				Id = 1
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
			Assert.Equal(expected, actual.Approved);
		}
	}
}
