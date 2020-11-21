using MediatR;

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
