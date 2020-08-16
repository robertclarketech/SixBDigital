namespace SixBDigital.Domain.Commands
{
	using MediatR;

	public class ToggleBookingApprovedCommand : IRequest
	{
		public int Id { get; set; }
	}
}
