namespace SixBDigital.Domain.Tests.Builders
{
	using System;
	using BCrypt.Net;
	using FluentValidation;
	using SixBDigital.Domain.Builders;
	using Xunit;

	public class UserBuilderTests
	{
		[Fact]
		public void Build_WithoutSettingAnyProperties_Throws()
		{
			//arrange
			var builder = new UserBuilder();

			//act + assert
			Assert.Throws<ValidationException>(builder.Build);
		}

		[Fact]
		public void SetUsername_WithNullUsername_Throws()
		{
			//arrange
			var builder = new UserBuilder();

			//act + assert
			Assert.Throws<ArgumentNullException>(() => builder.SetUsername(null));
		}

		[Fact]
		public void SetPassword_WithNullPassword_Throws()
		{
			//arrange
			var builder = new UserBuilder();

			//act + assert
			Assert.Throws<ArgumentNullException>(() => builder.SetPassword(null));
		}

		[Fact]
		public void Build_WithValidProperties_ReturnsAUserWithExpectedValues()
		{
			//arrange
			const string expectedUsername = "User";
			const string expectedPassword = "Password";

			var builder = new UserBuilder();
			builder.SetUsername(expectedUsername);
			builder.SetPassword(expectedPassword);

			//act
			var user = builder.Build();

			//assert
			Assert.Equal(expectedUsername, user.Username);
			Assert.True(BCrypt.EnhancedVerify(expectedPassword, user.Password));
		}
	}
}
