using ECommerceApp.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace ECommerceApp.Domain.Common
{
	public abstract class QueryDto
	{
		public IEnumerable<ErrorResponse> Errors { get; set; } = new List<ErrorResponse>();
	}
}
