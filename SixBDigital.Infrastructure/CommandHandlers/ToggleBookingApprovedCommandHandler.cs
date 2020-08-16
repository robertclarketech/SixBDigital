namespace SixBDigital.Infrastructure.CommandHandlers
{
	using System.Threading;
	using System.Threading.Tasks;
	using MediatR;
	using Microsoft.EntityFrameworkCore;
	using SixBDigital.Domain.Commands;
	using SixBDigital.Infrastructure.EntityFramework;

	public class ToggleBookingApprovedCommandHandler : AsyncRequestHandler<ToggleBookingApprovedCommand>
	{
		private readonly SixBDigitalContext _context;

		public ToggleBookingApprovedCommandHandler(SixBDigitalContext context)
		{
			_context = context;
		}

		protected override async Task Handle(ToggleBookingApprovedCommand request, CancellationToken cancellationToken)
		{
			var booking = await _context.Bookings.FirstOrDefaultAsync(e => e.Id == request.Id);
			booking.UpdateApproved(!booking.Approved);
			_context.Bookings!.Update(booking);
			await _context.SaveChangesAsync();
		}
	}
}
