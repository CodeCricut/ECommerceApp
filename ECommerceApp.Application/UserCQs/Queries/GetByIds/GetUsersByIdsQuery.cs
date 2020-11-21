using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.Common.Requests;
using ECommerceApp.Domain.Common.Models;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceApp.Application.UserCQs.Queries.GetByIds
{
	public class GetUsersByIdsQuery : IRequest<PaginatedList<UserQueryDto>>
	{
		public GetUsersByIdsQuery(IEnumerable<int> ids, PagingParams pagingParams)
		{
			Ids = ids;
			PagingParams = pagingParams;
		}

		public IEnumerable<int> Ids { get; }
		public PagingParams PagingParams { get; }
	}

	public class GetUserByIdsQueryHandler : DatabaseRequestHandler<
		GetUsersByIdsQuery,
		PaginatedList<UserQueryDto>
		>
	{
		public GetUserByIdsQueryHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override Task<PaginatedList<UserQueryDto>> Handle(GetUsersByIdsQuery request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
