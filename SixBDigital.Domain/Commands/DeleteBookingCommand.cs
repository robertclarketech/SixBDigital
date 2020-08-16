namespace SixBDigital.Domain.Commands
{
	using MediatR;

	public class DeleteBookingCommand : IRequest
	{
		public int Id { get; set; }
	}
}
