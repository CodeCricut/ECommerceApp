using System;

namespace ECommerceApp.Domain.Exceptions
{
	public class UnauthorizedException : Exception
	{
		public UnauthorizedException(string message = "Unauthorized to access the requested resource.") : base(message)
		{
		}
	}
}
