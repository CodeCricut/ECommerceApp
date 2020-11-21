using MediatR;
using System.Collections.Generic;

namespace ECommerceApp.Application.Common.Requests
{
	public class GetByIdsQuery<TResponse> : IRequest<TResponse>
	{
		public GetByIdsQuery(IEnumerable<int> ids)
		{
			Ids = ids;
		}

		public IEnumerable<int> Ids { get; }
	}
}
