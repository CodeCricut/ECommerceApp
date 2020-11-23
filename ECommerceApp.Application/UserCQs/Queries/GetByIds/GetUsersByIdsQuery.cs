using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.Common.Requests;
using ECommerceApp.Domain.Common.Mapping;
using ECommerceApp.Domain.Common.Models;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
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

	public class GetUsersByIdsHandler : DatabaseRequestHandler<
		GetUsersByIdsQuery,
		PaginatedList<UserQueryDto>
		>
	{
		public GetUsersByIdsHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<PaginatedList<UserQueryDto>> Handle(GetUsersByIdsQuery request, CancellationToken cancellationToken)
		{
			var users = await UnitOfWork.Users.GetEntitiesAsync();
			var paginatedUsersByIds = await users.Where(u => request.Ids.Contains(u.Id) && ! u.Deleted).ToPaginatedListAsync(request.PagingParams);

			var paginatedUserQueryDtos = await paginatedUsersByIds.ToMappedPaginatedListAsync<User, UserQueryDto>(Mapper);
			foreach (var response in paginatedUserQueryDtos.Items)
			{
				// TODO: this should probably be dealt with elsewhere.
				response.Bought = null;
				response.Saved = null;
				response.ShoppingCartItems = null;
			}

			return paginatedUserQueryDtos;
		}
	}
}
