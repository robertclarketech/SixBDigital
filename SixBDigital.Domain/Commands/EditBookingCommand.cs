namespace SixBDigital.Domain.Commands
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using MediatR;
	using SixBDigital.Domain.Entities;
	using SixBDigital.Domain.Enums;

	public class EditBookingCommand : IRequest
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Please enter the customers name"), Display(Name = "Your Name")]
		public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = "Please enter a booking date"), Display(Name = "Booking Date")]
		public DateTime BookingDate { get; set; } = DateTime.Now;

		[Required(ErrorMessage = "Please set a flexibility"), Display(Name = "Flexibility")]
		public Flexibility Flexibility { get; set; }

		[Required(ErrorMessage = "Please set a vehicle size"), Display(Name = "Vehicle Size")]
		public VehicleSize VehicleSize { get; set; }

		[Required(ErrorMessage = "Please enter the customers contact number"), Phone, Display(Name = "Contact Number")]
		public string ContactNumber { get; set; } = string.Empty;

		[Required(ErrorMessage = "Please enter the customers email address"),
			EmailAddress(ErrorMessage = "Please enter a valid email address"),
			Display(Name = "Email Address")]
		public string EmailAddress { get; set; } = string.Empty;

		[Required(ErrorMessage = "Please set whether the booking is approved"), Display(Name = "Approved")]
		public bool Approved { get; set; }

		public static implicit operator EditBookingCommand(Booking booking) => new EditBookingCommand
		{
			Id = booking.Id,
			BookingDate = booking.BookingDate,
			ContactNumber = booking.ContactNumber,
			EmailAddress = booking.EmailAddress,
			Flexibility = booking.Flexibility,
			Name = booking.Name,
			VehicleSize = booking.VehicleSize,
			Approved = booking.Approved
		};
	}
}
