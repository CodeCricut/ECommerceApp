using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.Common.Requests;
using ECommerceApp.Domain.Common.Models;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceApp.Application.UserCQs.Queries.GetBySearch
{
	public class GetUsersBySearchQuery : IRequest<PaginatedList<UserQueryDto>>
	{
		public GetUsersBySearchQuery(string searchTerm, PagingParams pagingParams)
		{
			SearchTerm = searchTerm;
			PagingParams = pagingParams;
		}

		public string SearchTerm { get; }
		public PagingParams PagingParams { get; }
	}

	public class GetUsersBySearchHandler : DatabaseRequestHandler<
		GetUsersBySearchQuery,
		PaginatedList<UserQueryDto>
		>
	{
		public GetUsersBySearchHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override Task<PaginatedList<UserQueryDto>> Handle(GetUsersBySearchQuery request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
