namespace SixBDigital.Domain.Exceptions
{
	using System;
	using System.Runtime.Serialization;

	[Serializable]
	public class BookingNotFoundException : Exception
	{
		public BookingNotFoundException()
		{
		}

		public BookingNotFoundException(string message) : base(message)
		{
		}

		public BookingNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected BookingNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
