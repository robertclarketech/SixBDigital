namespace SixBDigital.Infrastructure.CommandHandlers
{
	using System.Threading;
	using System.Threading.Tasks;
	using MediatR;
	using SixBDigital.Domain.Builders;
	using SixBDigital.Domain.Commands;
	using SixBDigital.Infrastructure.EntityFramework;

	public class CreateBookingCommandHandler : AsyncRequestHandler<CreateBookingCommand>
	{
		private readonly SixBDigitalContext _context;

		public CreateBookingCommandHandler(SixBDigitalContext context)
		{
			_context = context;
		}

		protected override async Task Handle(CreateBookingCommand request, CancellationToken cancellationToken)
		{
			var booking = new BookingBuilder()
				.SetBookingDate(request.BookingDate)
				.SetContactNumber(request.ContactNumber)
				.SetEmailAddress(request.EmailAddress)
				.SetFlexibility(request.Flexibility)
				.SetName(request.Name)
				.SetVehicleSize(request.VehicleSize)
				.Build();

			_context.Add(booking);
			_ = await _context
				.SaveChangesAsync()
				.ConfigureAwait(false);
		}
	}
}
