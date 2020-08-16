namespace SixBDigital.Web.Controllers
{
	using System.Collections.Generic;
	using System.Security.Claims;
	using System.Threading.Tasks;
	using MediatR;
	using Microsoft.AspNetCore.Authentication;
	using Microsoft.AspNetCore.Authentication.Cookies;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using SixBDigital.Domain.Commands;
	using SixBDigital.Domain.Entities;
	using SixBDigital.Domain.Exceptions;
	using SixBDigital.Domain.Queries;
	using SixBDigital.Web.ViewModels.Admin;

	[Authorize(Roles = "Administrator")]
	[Route("[controller]")]
	public class AdminController : Controller
	{
		private readonly IMediator _mediator;

		public AdminController(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task<IActionResult> Index()
		{
			var bookings = await _mediator
				.Send(new GetAllBookingsQuery())
				.ConfigureAwait(false);

			return View(new AdminIndexViewModel { Bookings = bookings });
		}

		[HttpPost("Delete/{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			_ = await _mediator
				.Send(new DeleteBookingCommand { Id = id })
				.ConfigureAwait(false);

			return Ok();
		}

		[HttpGet("Edit/{id}")]
		public async Task<IActionResult> Edit(int id)
		{
			var booking = await _mediator
				.Send(new GetBookingQuery { Id = id })
				.ConfigureAwait(false);

			return View((EditBookingCommand)booking);
		}

		[HttpPost("Edit/{id}")]
		public async Task<IActionResult> Edit(EditBookingCommand command)
		{
			if (!ModelState.IsValid)
			{
				return View(command);
			}

			_ = await _mediator
				.Send(command)
				.ConfigureAwait(false);

			return RedirectToAction("Index");
		}

		[HttpPost("ToggleApproval/{id}")]
		public async Task<IActionResult> ToggleApproval(int id)
		{
			_ = await _mediator
				.Send(new ToggleBookingApprovedCommand { Id = id })
				.ConfigureAwait(false);

			return RedirectToAction("Index");
		}

		[HttpGet("Create")]
		public IActionResult Create()
		{
			return View(new CreateBookingCommand());
		}

		[HttpPost("Create")]
		public async Task<IActionResult> Create(CreateBookingCommand command)
		{
			if (!ModelState.IsValid)
			{
				return View(command);
			}

			_ = await _mediator
				.Send(command)
				.ConfigureAwait(false);

			return RedirectToAction("Index");
		}

		[AllowAnonymous]
		[HttpGet("LogIn")]
		public IActionResult LogIn()
		{
			return View(new AdminLogInQuery());
		}

		[HttpPost("LogIn"), AllowAnonymous]
		public async Task<IActionResult> LogIn(AdminLogInQuery command, [FromQuery(Name = "ReturnUrl")]string? returnUrl)
		{
			if (!ModelState.IsValid)
			{
				return View(command);
			}

			User user;
			try
			{
				user = await _mediator
					.Send(command)
					.ConfigureAwait(false);
			}
			catch (FailedLoginException)
			{
				ModelState.AddModelError(string.Empty, "Username or Password was incorrect");
				return View(command);
			}

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Username),
				new Claim(ClaimTypes.Role, "Administrator"),
			};

			var claimsIdentity = new ClaimsIdentity(
				claims,
				CookieAuthenticationDefaults.AuthenticationScheme);

			await HttpContext
				.SignInAsync(
					CookieAuthenticationDefaults.AuthenticationScheme,
					new ClaimsPrincipal(claimsIdentity))
				.ConfigureAwait(false);

			if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}

			return RedirectToAction("Index");
		}

		[HttpGet("LogOut")]
		public async Task<IActionResult> LogOut()
		{
			await HttpContext
				.SignOutAsync()
				.ConfigureAwait(false);
			return RedirectToAction("Index", "Home");
		}
	}
}
