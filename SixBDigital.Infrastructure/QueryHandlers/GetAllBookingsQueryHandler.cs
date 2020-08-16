namespace SixBDigital.Infrastructure.QueryHandlers
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;
	using MediatR;
	using Microsoft.EntityFrameworkCore;
	using SixBDigital.Domain.Entities;
	using SixBDigital.Domain.Queries;
	using SixBDigital.Infrastructure.EntityFramework;

	public class GetAllBookingsQueryHandler : IRequestHandler<GetAllBookingsQuery, List<Booking>>
	{
		private readonly SixBDigitalContext _context;

		public GetAllBookingsQueryHandler(SixBDigitalContext context)
		{
			_context = context;
		}

		public Task<List<Booking>> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
		{
			return _context.Bookings.ToListAsync();
		}
	}
}
