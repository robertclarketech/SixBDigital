namespace SixBDigital.Domain.Queries
{
	using MediatR;
	using SixBDigital.Domain.Entities;

	public class GetBookingQuery : IRequest<Booking>
	{
		public int Id { get; set; }
	}
}
