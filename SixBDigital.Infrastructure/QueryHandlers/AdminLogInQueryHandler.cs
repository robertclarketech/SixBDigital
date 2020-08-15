namespace SixBDigital.Infrastructure.QueryHandlers
{
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;
	using MediatR;
	using SixBDigital.Domain.Entities;
	using SixBDigital.Domain.Queries;
	using SixBDigital.Infrastructure.EntityFramework;
	using BCrypt.Net;
	using SixBDigital.Domain.Exceptions;
	using Microsoft.EntityFrameworkCore;

	public class AdminLogInQueryHandler : IRequestHandler<AdminLogInQuery, User>
	{
		private readonly SixBDigitalContext _context;

		public AdminLogInQueryHandler(SixBDigitalContext context)
		{
			_context = context;
		}

		public Task<User> Handle(AdminLogInQuery request, CancellationToken cancellationToken)
		{
			var user = _context
				.Users
				.FirstOrDefault(e => EF.Functions.Like(e.Username, $"%{request.Username}%"));

			if (user == null || !BCrypt.EnhancedVerify(request.Password, user.Password))
			{
				throw new FailedLoginException();
			}
			return Task.FromResult(user);
		}
	}
}
