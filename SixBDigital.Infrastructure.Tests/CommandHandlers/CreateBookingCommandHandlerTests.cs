namespace SixBDigital.Infrastructure.Tests.CommandHandlers
{
	using System;
	using System.Threading.Tasks;
	using MediatR;
	using Microsoft.EntityFrameworkCore;
	using SixBDigital.Domain.Commands;
	using SixBDigital.Domain.Enums;
	using SixBDigital.Infrastructure.CommandHandlers;
	using SixBDigital.Infrastructure.Tests.Abstracts;
	using Xunit;

	public class CreateBookingCommandHandlerTests : InMemoryDbContextBase
	{
		private readonly IRequestHandler<CreateBookingCommand> _handler;

		public CreateBookingCommandHandlerTests()
		{
			_handler = new CreateBookingCommandHandler(Context);
		}

		[Fact]
		public async Task Handle_WithAValidBookingCommand_AddsABookingToTheDatabase()
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
		}
	}
}
