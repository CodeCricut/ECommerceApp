using System;

namespace ECommerceApp.Domain.Exceptions
{
	public class InvalidPostException : Exception
	{
		private const string DEFAULT_MESSAGE = "Invalid post request.";
		public InvalidPostException(string message = DEFAULT_MESSAGE)
			: base(message)
		{
		}
	}
}
