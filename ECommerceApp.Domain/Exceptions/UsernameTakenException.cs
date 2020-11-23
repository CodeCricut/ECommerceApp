using System;

namespace ECommerceApp.Domain.Exceptions
{
	public class UsernameTakenException : Exception
	{
		public UsernameTakenException(string message = "Username taken.") : base(message)
		{
		}
	}
}
