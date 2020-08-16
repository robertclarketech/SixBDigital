namespace SixBDigital.Infrastructure.CommandHandlers
{
	using System.Threading;
	using System.Threading.Tasks;
	using MediatR;
	using Microsoft.EntityFrameworkCore;
	using SixBDigital.Domain.Commands;
	using SixBDigital.Domain.Exceptions;
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
			var booking = await _context
				.Bookings
				.FirstOrDefaultAsync(e => e.Id == request.Id)
				.ConfigureAwait(false);

			if (booking == null)
			{
				throw new BookingNotFoundException();
			}

			booking.UpdateApproved(!booking.Approved);

			_context.Bookings!.Update(booking);
			_ = await _context
				.SaveChangesAsync()
				.ConfigureAwait(false);
		}
	}
}
