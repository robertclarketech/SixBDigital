namespace SixBDigital.Web.Controllers
{
	using System.Threading.Tasks;
	using FluentValidation;
	using MediatR;
	using Microsoft.AspNetCore.Mvc;
	using SixBDigital.Domain.Commands;

	public class BookingController : Controller
	{
		private readonly IMediator _mediator;

		public BookingController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View(new CreateBookingCommand());
		}

		[HttpPost]
		public async Task<IActionResult> Index(CreateBookingCommand command)
		{
			if (!ModelState.IsValid)
			{
				return View(command);
			}

			_ = await _mediator
				.Send(command)
				.ConfigureAwait(false);

			return View("Success");
		}
	}
}
