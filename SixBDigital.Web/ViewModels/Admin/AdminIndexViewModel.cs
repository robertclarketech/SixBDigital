namespace SixBDigital.Web.ViewModels.Admin
{
	using System.Collections.Generic;
	using SixBDigital.Domain.Entities;

	public class AdminIndexViewModel
	{
		public List<Booking> Bookings { get; set; } = new List<Booking>();
	}
}
