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

		public override Task<PaginatedList<UserQueryDto>> Handle(GetUsersByUsernamesQuery request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
