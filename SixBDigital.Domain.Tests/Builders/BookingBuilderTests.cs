namespace SixBDigital.Domain.Tests.Builders
{
	using System;
	using FluentValidation;
	using SixBDigital.Domain.Builders;
	using SixBDigital.Domain.Enums;
	using Xunit;

	public class BookingBuilderTests
	{
		[Fact]
		public void Build_WithoutSettingAnyProperties_Throws()
		{
			//arrange
			var builder = new BookingBuilder();

			//act + assert
			Assert.Throws<ValidationException>(builder.Build);
		}

		[Fact]
		public void SetName_WithANullName_Throws()
		{
			//arrange
			var builder = new BookingBuilder();

			//act + assert
			Assert.Throws<ArgumentNullException>(() => builder.SetName(null));
		}

		[Fact]
		public void SetContactNumber_WithANullContactNumber_Throws()
		{
			//arrange
			var builder = new BookingBuilder();

			//act + assert
			Assert.Throws<ArgumentNullException>(() => builder.SetContactNumber(null));
		}

		[Fact]
		public void SetEmailAddress_WithANullEmailAddress_Throws()
		{
			//arrange
			var builder = new BookingBuilder();

			//act + assert
			Assert.Throws<ArgumentNullException>(() => builder.SetEmailAddress(null));
		}

		[Fact]
		public void Build_WithValidProperties_ReturnsABookingWithExpectedValues()
		{
			//arrange
			DateTime expectedBookingDate = DateTime.Now;
			const string expectedContactNumber = "0123456789";
			const string expectedEmailAddress = "test@email.com";
			const Flexibility expectedFlexibility = Flexibility.PlusMinusThreeDays;
			const string expectedName = "Name";
			const VehicleSize expectedVehicleSize = VehicleSize.Van;

			var builder = new BookingBuilder();
			builder.SetBookingDate(expectedBookingDate);
			builder.SetContactNumber(expectedContactNumber);
			builder.SetEmailAddress(expectedEmailAddress);
			builder.SetFlexibility(expectedFlexibility);
			builder.SetName(expectedName);
			builder.SetVehicleSize(expectedVehicleSize);

			//act
			var booking = builder.Build();

			//assert
			Assert.Equal(expectedBookingDate, booking.BookingDate);
			Assert.Equal(expectedContactNumber, booking.ContactNumber);
			Assert.Equal(expectedEmailAddress, booking.EmailAddress);
			Assert.Equal(expectedFlexibility, booking.Flexibility);
			Assert.Equal(expectedName, booking.Name);
			Assert.Equal(expectedVehicleSize, booking.VehicleSize);
		}
	}
}
