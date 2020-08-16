namespace SixBDigital.Infrastructure.QueryHandlers
{
	using System.Threading;
	using System.Threading.Tasks;
	using MediatR;
	using Microsoft.EntityFrameworkCore;
	using SixBDigital.Domain.Entities;
	using SixBDigital.Domain.Exceptions;
	using SixBDigital.Domain.Queries;
	using SixBDigital.Infrastructure.EntityFramework;

	public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, Booking>
	{
		private readonly SixBDigitalContext _context;

		public GetBookingQueryHandler(SixBDigitalContext context)
		{
			_context = context;
		}

		public async Task<Booking> Handle(GetBookingQuery request, CancellationToken cancellationToken)
		{
			var booking = await _context
				.Bookings
				.FirstOrDefaultAsync(e => e.Id == request.Id)
				.ConfigureAwait(false);

			if (booking == null)
			{
				throw new BookingNotFoundException();
			}
			return booking;
		}
	}
}
