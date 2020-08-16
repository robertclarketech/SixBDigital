namespace SixBDigital.Domain.Tests.Entities
{
	using System;
	using FluentValidation;
	using SixBDigital.Domain.Entities;
	using SixBDigital.Domain.Enums;
	using Xunit;

	public class BookingTests
	{
		private readonly Booking _validBooking;

		public BookingTests()
		{
			_validBooking = new Booking
			{
				Approved = false,
				BookingDate = DateTime.Now,
				ContactNumber = "0123456789",
				EmailAddress = "email@address.com",
				Flexibility = Flexibility.PlusMinusOneDay,
				Name = "Name",
				VehicleSize = VehicleSize.Large
			};
		}

		[Fact]
		public void UpdateName_WithANullName_Throws()
		{
			//arrange

			//act + assert
			Assert.Throws<ArgumentNullException>(() => _validBooking.UpdateName(null));
		}

		[Fact]
		public void UpdateName_WithAnEmptyName_Throws()
		{
			//arrange

			//act + assert
			Assert.Throws<ValidationException>(() => _validBooking.UpdateName(string.Empty));
		}

		[Fact]
		public void UpdateName_WithWhiteSpace_Throws()
		{
			//arrange

			//act + assert
			Assert.Throws<ValidationException>(() => _validBooking.UpdateName("      "));
		}

		[Fact]
		public void UpdateName_WithAValidName_UpdatesTheName()
		{
			//arrange
			const string expectedName = "NewName";

			//act
			var booking = _validBooking.UpdateName(expectedName);

			//assert
			Assert.Equal(expectedName, booking.Name);
		}

		[Fact]
		public void UpdateContactNumber_WithANullContactNumber_Throws()
		{
			//arrange

			//act + assert
			Assert.Throws<ArgumentNullException>(() => _validBooking.UpdateContactNumber(null));
		}

		[Fact]
		public void UpdateContactNumber_WithAnEmptyString_Throws()
		{
			//arrange

			//act + assert
			Assert.Throws<ValidationException>(() => _validBooking.UpdateContactNumber(string.Empty));
		}

		[Fact]
		public void UpdateContactNumber_WithWhiteSpace_Throws()
		{
			//arrange

			//act + assert
			Assert.Throws<ValidationException>(() => _validBooking.UpdateContactNumber("      "));
		}

		[Fact]
		public void UpdateContactNumber_WithAValidNumber_UpdatesTheContactNumber()
		{
			//arrange
			const string expectedNumber = "9876543210";

			//act
			var booking = _validBooking.UpdateContactNumber(expectedNumber);

			//assert
			Assert.Equal(expectedNumber, booking.ContactNumber);
		}

		[Fact]
		public void UpdateEmailAddress_WithANullEmailAddress_Throws()
		{
			//arrange

			//act + assert
			Assert.Throws<ArgumentNullException>(() => _validBooking.UpdateEmailAddress(null));
		}

		[Fact]
		public void UpdateEmailAddress_WithAnEmptyString_Throws()
		{
			//arrange

			//act + assert
			Assert.Throws<ValidationException>(() => _validBooking.UpdateEmailAddress(string.Empty));
		}

		[Fact]
		public void UpdateEmailAddress_WithWhiteSpace_Throws()
		{
			//arrange

			//act + assert
			Assert.Throws<ValidationException>(() => _validBooking.UpdateEmailAddress("      "));
		}

		[Fact]
		public void UpdateEmailAddress_WithAValidEmail_UpdatesTheEmailAddress()
		{
			//arrange
			const string expectedEmail = "NewEmail@Address.com";

			//act
			var booking = _validBooking.UpdateEmailAddress(expectedEmail);

			//assert
			Assert.Equal(expectedEmail, booking.EmailAddress);
		}

		[Fact]
		public void UpdateApproved_WithAValidApproval_UpdatesTheApproval()
		{
			//arrange
			var expectedApproved = !_validBooking.Approved;

			//act
			var booking = _validBooking.UpdateApproved(expectedApproved);

			//assert
			Assert.Equal(expectedApproved, booking.Approved);
		}

		[Fact]
		public void UpdateBookingDate_WithAValidBookingDate_UpdatesTheBookingDate()
		{
			//arrange
			var expectedDate = _validBooking.BookingDate.AddMonths(1);

			//act
			var booking = _validBooking.UpdateBookingDate(expectedDate);

			//assert
			Assert.Equal(expectedDate, booking.BookingDate);
		}

		[Fact]
		public void UpdateFlexibility_WithAValidFlexibility_UpdatesTheFlexibility()
		{
			//arrange
			const Flexibility expectedFlexibility = Flexibility.PlusMinusThreeDays;

			//act
			var booking = _validBooking.UpdateFlexibility(expectedFlexibility);

			//assert
			Assert.Equal(expectedFlexibility, booking.Flexibility);
		}

		[Fact]
		public void UpdateVehicleSize_WithAValidVehicleSize_UpdatesTheVehicleSize()
		{
			//arrange
			const VehicleSize expectedSize = VehicleSize.Medium;

			//act
			var booking = _validBooking.UpdateVehicleSize(expectedSize);

			//assert
			Assert.Equal(expectedSize, booking.VehicleSize);
		}

		[Fact]
		public void Validate_WithAValidBooking_ReturnsNoValidationErrors()
		{
			//arrange
			const bool expected = true;

			//act
			var actual = _validBooking.Validate();

			//assert
			Assert.Equal(expected, actual.IsValid);
		}
	}
}
