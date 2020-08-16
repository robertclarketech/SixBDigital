namespace SixBDigital.Infrastructure.CommandHandlers
{
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;
	using MediatR;
	using Microsoft.EntityFrameworkCore;
	using SixBDigital.Domain.Commands;
	using SixBDigital.Domain.Exceptions;
	using SixBDigital.Infrastructure.EntityFramework;

	public class DeleteBookingCommandHandler : AsyncRequestHandler<DeleteBookingCommand>
	{
		private readonly SixBDigitalContext _context;

		public DeleteBookingCommandHandler(SixBDigitalContext context)
		{
			_context = context;
		}

		protected override async Task Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
		{
			var booking = await _context
				.Bookings
				.FirstOrDefaultAsync(e => e.Id == request.Id)
				.ConfigureAwait(false);

			if (booking == null)
			{
				throw new BookingNotFoundException();
			}

			_ = _context
				.Bookings!
				.Remove(booking);

			_ = await _context
				.SaveChangesAsync()
				.ConfigureAwait(false);
		}
	}
}
