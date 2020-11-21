﻿using System;

namespace ECommerceApp.Domain.Exceptions
{
	public class ErrorResponse
	{
		public string Type { get; set; }
		public string Message { get; set; }
		public string StackTrace { get; set; }
		public object Errors { get; set; }

		public ErrorResponse(Exception e)
		{
			Type = e.GetType().Name;
			Message = e.Message;
			StackTrace = e.ToString();
			Errors = e.Data[ApiExceptionData.ERRORS];
		}
	}
}