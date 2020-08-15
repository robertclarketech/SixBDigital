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
	using SixBDigital.Domain.Entities;
	using SixBDigital.Domain.Exceptions;
	using SixBDigital.Domain.Queries;

	[Authorize(Roles = "Administrator")]
	public class AdminController : Controller
	{
		private readonly IMediator _mediator;

		public AdminController(IMediator mediator)
		{
			_mediator = mediator;
		}

		public IActionResult Index()
		{
			return View();
		}

		[AllowAnonymous]
		public IActionResult LogIn()
		{
			return View(new AdminLogInQuery());
		}

		[HttpPost, AllowAnonymous]
		public async Task<IActionResult> LogIn(AdminLogInQuery command, [FromQuery(Name = "ReturnUrl")]string returnUrl)
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
	}
}
