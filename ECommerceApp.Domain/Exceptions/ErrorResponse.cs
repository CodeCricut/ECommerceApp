using System;

namespace ECommerceApp.Domain.Exceptions
{
	public class ErrorResponse
	{
		public string TypeName { get; set; }
		public string Message { get; set; }

		public ErrorResponse(Exception e)
		{
			TypeName = e.GetType().Name;
			Message = e.Message;
		}
	}
}
