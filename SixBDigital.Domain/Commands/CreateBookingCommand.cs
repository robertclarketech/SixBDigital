namespace SixBDigital.Domain.Commands
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using MediatR;
	using SixBDigital.Domain.Enums;

	public class CreateBookingCommand : IRequest
	{
		[Required]
		[Display(Name = "Your Name")]
		public string Name { get; set; } = string.Empty;

		[Required]
		[Display(Name = "Booking Date")]
		public DateTime BookingDate { get; set; } = DateTime.Now;

		[Required]
		[Display(Name = "Flexibility")]
		public Flexibility Flexibility { get; set; }

		[Required]
		[Display(Name = "Vehicle Size")]
		public VehicleSize VehicleSize { get; set; }

		[Required]
		[Phone]
		[Display(Name = "Contact Number")]
		public string ContactNumber { get; set; } = string.Empty;

		[Required]
		[EmailAddress]
		[Display(Name = "Email Address")]
		public string EmailAddress { get; set; } = string.Empty;
	}
}
