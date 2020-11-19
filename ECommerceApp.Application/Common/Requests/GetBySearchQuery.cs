using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.Application.Common.Requests
{
	public class GetBySearchQuery<TResponse> : IRequest<TResponse>
	{
		public GetBySearchQuery(string searchTerm)
		{
			SearchTerm = searchTerm;
		}

		public string SearchTerm { get; }
	}
}
