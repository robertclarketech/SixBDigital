namespace SixBDigital.Infrastructure.Tests.QueryHandlers
{
	using System.Threading.Tasks;
	using MediatR;
	using SixBDigital.Domain.Entities;
	using SixBDigital.Domain.Exceptions;
	using SixBDigital.Domain.Queries;
	using SixBDigital.Infrastructure.QueryHandlers;
	using SixBDigital.Infrastructure.Tests.Abstracts;
	using Xunit;

	public class AdminLogInQueryHandlerTests : InMemoryDbContextBase
	{
		private readonly IRequestHandler<AdminLogInQuery, User> _handler;

		public AdminLogInQueryHandlerTests()
		{
			_handler = new AdminLogInQueryHandler(Context);
		}

		[Fact]
		public async Task Handle_WithNoUsers_Throws()
		{
			//arrange
			var invalidQuery = new AdminLogInQuery
			{
				Password = string.Empty,
				Username = string.Empty
			};

			// act + assert
			_ = await Assert
				.ThrowsAsync<FailedLoginException>(() => _handler.Handle(invalidQuery, default))
				.ConfigureAwait(false);
		}

		[Fact]
		public async Task Handle_WithAnInvalidUsername_Throws()
		{
			//arrange
			Context.Users.Add(new User { Username = "User", Password = string.Empty });
			_ = await Context
				.SaveChangesAsync()
				.ConfigureAwait(false);

			var invalidQuery = new AdminLogInQuery
			{
				Password = string.Empty,
				Username = "DifferentUser"
			};

			// act + assert
			_ = await Assert
				.ThrowsAsync<FailedLoginException>(() => _handler.Handle(invalidQuery, default))
				.ConfigureAwait(false);
		}

		[Fact]
		public async Task Handle_WithAnInvalidPassword_Throws()
		{
			//arrange
			const string username = "User";
			Context.Users.Add(new User { Username = username, Password = BCrypt.Net.BCrypt.EnhancedHashPassword("password") });
			_ = await Context
				.SaveChangesAsync()
				.ConfigureAwait(false);
			var invalidQuery = new AdminLogInQuery
			{
				Password = "test123",
				Username = username
			};

			// act + assert
			_ = await Assert
				.ThrowsAsync<FailedLoginException>(() => _handler.Handle(invalidQuery, default))
				.ConfigureAwait(false);
		}

		[Fact]
		public async Task Handle_WithAValidUserAndPassword_ReturnsUser()
		{
			//arrange
			const string password = "password";
			const string username = "User";
			Context.Users.Add(new User { Username = username, Password = BCrypt.Net.BCrypt.EnhancedHashPassword(password) });
			_ = await Context
				.SaveChangesAsync()
				.ConfigureAwait(false);

			var validQuery = new AdminLogInQuery
			{
				Password = password,
				Username = username
			};

			// act
			var user = await _handler.Handle(validQuery, default).ConfigureAwait(false);

			//assert
			Assert.Equal(username, user.Username);
			Assert.True(BCrypt.Net.BCrypt.EnhancedVerify(password, user.Password));
		}

		[Fact]
		public async Task Handle_WithDifferentCaseUsernames_ReturnsUser()
		{
			//arrange
			const string password = "password";
			const string username = "User";
			Context.Users.Add(new User { Username = username.ToLower(), Password = BCrypt.Net.BCrypt.EnhancedHashPassword(password) });
			_ = await Context
				.SaveChangesAsync()
				.ConfigureAwait(false);

			var validQuery = new AdminLogInQuery
			{
				Password = password,
				Username = username.ToUpper()
			};

			// act
			var user = await _handler.Handle(validQuery, default).ConfigureAwait(false);

			//assert
			Assert.True(string.Equals(username, user.Username, System.StringComparison.OrdinalIgnoreCase));
			Assert.True(BCrypt.Net.BCrypt.EnhancedVerify(password, user.Password));
		}
	}
}
