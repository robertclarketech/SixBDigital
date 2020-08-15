namespace SixBDigital.Domain.Exceptions
{
	using System;
	using System.Runtime.Serialization;

	[Serializable]
	public class FailedLoginException : Exception
	{
		public FailedLoginException()
		{
		}

		public FailedLoginException(string message) : base(message)
		{
		}

		public FailedLoginException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected FailedLoginException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
