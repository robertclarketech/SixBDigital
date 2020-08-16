namespace SixBDigital.Infrastructure.Tests.CommandHandlers
{
	using System.Threading.Tasks;
	using MediatR;
	using SixBDigital.Domain.Commands;
	using SixBDigital.Domain.Entities;
	using SixBDigital.Domain.Exceptions;
	using SixBDigital.Infrastructure.CommandHandlers;
	using SixBDigital.Infrastructure.Tests.Abstracts;
	using Xunit;

	public class DeleteBookingCommandHandlerTests : InMemoryDbContextBase
	{
		private readonly IRequestHandler<DeleteBookingCommand> _handler;

		public DeleteBookingCommandHandlerTests()
		{
			_handler = new DeleteBookingCommandHandler(Context);
		}

		[Fact]
		public async Task Handle_WithAnInvalidDeleteBookingCommand_Throws()
		{
			//arrange
			var invalidCommand = new DeleteBookingCommand { Id = 0 };

			//act + assert
			_ = await Assert
				.ThrowsAsync<BookingNotFoundException>(() => _handler.Handle(invalidCommand, default))
				.ConfigureAwait(false);
		}

		[Fact]
		public async Task Handle_WithAValidDeleteBookingCommand_RemovesBookingFromDatabase()
		{
			//arrange
			Context.Add(new Booking());
			_ = await Context
				.SaveChangesAsync()
				.ConfigureAwait(false);
			var validCommand = new DeleteBookingCommand { Id = 1 };

			//act
			_ = await _handler
				.Handle(validCommand, default)
				.ConfigureAwait(false);

			//assert
			Assert.Empty(Context.Bookings);
		}
	}
}
