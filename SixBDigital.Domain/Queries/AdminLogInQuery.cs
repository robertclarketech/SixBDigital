namespace SixBDigital.Domain.Queries
{
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using MediatR;
	using SixBDigital.Domain.Entities;

	public class AdminLogInQuery : IRequest<User>
	{
		[Required]
		public string Username { get; set; } = string.Empty;

		[Required]
		public string Password { get; set; } = string.Empty;
	}
}
