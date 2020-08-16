namespace SixBDigital.Infrastructure.Tests.QueryHandlers
{
	using System;
	using System.Threading.Tasks;
	using MediatR;
	using SixBDigital.Domain.Entities;
	using SixBDigital.Domain.Enums;
	using SixBDigital.Domain.Exceptions;
	using SixBDigital.Domain.Queries;
	using SixBDigital.Infrastructure.QueryHandlers;
	using SixBDigital.Infrastructure.Tests.Abstracts;
	using Xunit;

	public class GetBookingQueryHandlerTests : InMemoryDbContextBase
	{
		private readonly IRequestHandler<GetBookingQuery, Booking> _handler;

		public GetBookingQueryHandlerTests()
		{
			_handler = new GetBookingQueryHandler(Context);
		}

		[Fact]
		public async Task Handle_WithNoBookings_Throws()
		{
			//arrange
			var query = new GetBookingQuery { Id = 0 };

			//act + assert
			_ = await Assert
				.ThrowsAsync<BookingNotFoundException>(() => _handler.Handle(query, default))
				.ConfigureAwait(false);
		}

		[Fact]
		public async Task Handle_WithValidGetBookingQuery_ReturnsBooking()
		{
			//arrange
			var expected = new Booking
			{
				BookingDate = DateTime.Now,
				ContactNumber = "0123456789",
				EmailAddress = "email@address.com",
				Flexibility = Flexibility.PlusMinusOneDay,
				Name = "Name",
				VehicleSize = VehicleSize.Large,
				Approved = false
			};
			Context.Add(expected);
			_ = await Context
				.SaveChangesAsync()
				.ConfigureAwait(false);

			var query = new GetBookingQuery { Id = 1 };

			//act
			var actual = await _handler
				.Handle(query, default)
				.ConfigureAwait(false);

			//assert
			Assert.Equal(expected.BookingDate, actual.BookingDate);
			Assert.Equal(expected.ContactNumber, actual.ContactNumber);
			Assert.Equal(expected.EmailAddress, actual.EmailAddress);
			Assert.Equal(expected.Flexibility, actual.Flexibility);
			Assert.Equal(expected.Name, actual.Name);
			Assert.Equal(expected.VehicleSize, actual.VehicleSize);
			Assert.Equal(expected.Approved, actual.Approved);
		}
	}
}
