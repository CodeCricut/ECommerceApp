using System;

namespace ECommerceApp.Domain.Exceptions
{
	public class NotFoundException : Exception
	{
		public NotFoundException(string message = "Requested resource not found") : base(message)
		{
		}
	}
}
