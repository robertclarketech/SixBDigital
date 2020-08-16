namespace SixBDigital.Domain.Queries
{
	using System.Collections.Generic;
	using MediatR;
	using SixBDigital.Domain.Entities;

	public class GetAllBookingsQuery : IRequest<List<Booking>>
	{
	}
}
