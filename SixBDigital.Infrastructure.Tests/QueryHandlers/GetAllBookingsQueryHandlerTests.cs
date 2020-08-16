namespace SixBDigital.Infrastructure.Tests.QueryHandlers
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using MediatR;
	using SixBDigital.Domain.Entities;
	using SixBDigital.Domain.Queries;
	using SixBDigital.Infrastructure.QueryHandlers;
	using SixBDigital.Infrastructure.Tests.Abstracts;
	using Xunit;

	public class GetAllBookingsQueryHandlerTests : InMemoryDbContextBase
	{
		private readonly IRequestHandler<GetAllBookingsQuery, List<Booking>> _handler;

		public GetAllBookingsQueryHandlerTests()
		{
			_handler = new GetAllBookingsQueryHandler(Context);
		}

		[Fact]
		public async Task Handle_WhenThereAreNoBookings_ReturnsEmptyList()
		{
			//arrange
			var query = new GetAllBookingsQuery();

			//act
			var actual = await _handler
				.Handle(query, default)
				.ConfigureAwait(false);

			//assert
			Assert.Empty(actual);
		}

		[Fact]
		public async Task Handle_WhenThereIsOneBooking_ReturnsSingleBooking()
		{
			//arrange
			Context.Bookings.Add(new Booking());
			_ = await Context
				.SaveChangesAsync()
				.ConfigureAwait(false);

			var query = new GetAllBookingsQuery();

			//act
			var actual = await _handler
				.Handle(query, default)
				.ConfigureAwait(false);

			//assert
			Assert.Single(actual);
		}

		[Fact]
		public async Task Handle_WhenThereAreTwoBookings_ReturnsTwoBookings()
		{
			//arrange
			Context.Bookings.AddRange(new Booking(), new Booking());
			_ = await Context
				.SaveChangesAsync()
				.ConfigureAwait(false);

			var query = new GetAllBookingsQuery();

			//act
			var actual = await _handler
				.Handle(query, default)
				.ConfigureAwait(false);

			//assert
			Assert.Equal(2, actual.Count);
		}
	}
}
