namespace SixBDigital.Infrastructure.CommandHandlers
{
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;
	using MediatR;
	using Microsoft.EntityFrameworkCore;
	using SixBDigital.Domain.Commands;
	using SixBDigital.Infrastructure.EntityFramework;

	public class EditBookingCommandHandler : AsyncRequestHandler<EditBookingCommand>
	{
		private readonly SixBDigitalContext _context;

		public EditBookingCommandHandler(SixBDigitalContext context)
		{
			_context = context;
		}

		protected override async Task Handle(EditBookingCommand request, CancellationToken cancellationToken)
		{
			var booking = await _context
				.Bookings
				.FirstOrDefaultAsync(e => e.Id == request.Id)
				.ConfigureAwait(false);

			booking
				.UpdateBookingDate(request.BookingDate)
				.UpdateContactNumber(request.ContactNumber)
				.UpdateEmailAddress(request.EmailAddress)
				.UpdateFlexibility(request.Flexibility)
				.UpdateName(request.Name)
				.UpdateVehicleSize(request.VehicleSize)
				.UpdateApproved(request.Approved);

			_context.Bookings!.Update(booking);

			await _context
				.SaveChangesAsync()
				.ConfigureAwait(false);
		}
	}
}
