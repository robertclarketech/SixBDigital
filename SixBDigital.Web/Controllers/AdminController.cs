namespace SixBDigital.Web.Controllers
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Authorize]
	public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

		[AllowAnonymous]
		public IActionResult LogIn()
		{
			return View();
		}
	}
}
