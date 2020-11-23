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

namespace ECommerceApp.Application.UserCQs.Queries.GetByUsernames
{
	public class GetUsersByUsernamesQuery : IRequest<PaginatedList<UserQueryDto>>
	{
		public GetUsersByUsernamesQuery(IEnumerable<string> usernames, PagingParams pagingParams)
		{
			Usernames = usernames;
			PagingParams = pagingParams;
		}

		public IEnumerable<string> Usernames { get; }
		public PagingParams PagingParams { get; }
	}

	public class GetUsersByUsernamesHandler : DatabaseRequestHandler<
		GetUsersByUsernamesQuery,
		PaginatedList<UserQueryDto>>
	{
		public GetUsersByUsernamesHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<PaginatedList<UserQueryDto>> Handle(GetUsersByUsernamesQuery request, CancellationToken cancellationToken)
		{
			var users = await UnitOfWork.Users.GetEntitiesAsync();
			var paginatedUsersByUsernames = await users.Where(u => request.Usernames.Contains(u.Username)).ToPaginatedListAsync(request.PagingParams);
			var paginatedResponse = await paginatedUsersByUsernames.ToMappedPaginatedListAsync<User, UserQueryDto>(Mapper);

			return paginatedResponse;
		}
	}
}
