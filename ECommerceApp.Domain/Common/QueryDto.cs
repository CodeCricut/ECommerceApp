using System;
using System.Collections.Generic;

namespace ECommerceApp.Domain.Common
{
	public abstract class QueryDto
	{
		public IEnumerable<Exception> Exceptions { get; set; }
	}
}
