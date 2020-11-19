using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.Application.Common.Requests
{
	public class GetByIdQuery<TResponse> : IRequest<TResponse>
	{
		public GetByIdQuery(int id)
		{
			Id = id;
		}

		public int Id { get; }
	}
}
