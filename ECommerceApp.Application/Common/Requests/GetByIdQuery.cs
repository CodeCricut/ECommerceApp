using MediatR;

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
