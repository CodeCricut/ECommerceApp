using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.Common.Requests;
using ECommerceApp.Domain.Common.Models;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceApp.Application.UserCQs.Queries.GetBySearch
{
	public class GetUserBySearchQuery : DatabaseRequestHandler<
		GetBySearchQuery<PaginatedList<UserQueryDto>>,
		PaginatedList<UserQueryDto>
		>
	{
		public GetUserBySearchQuery(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override Task<PaginatedList<UserQueryDto>> Handle(GetBySearchQuery<PaginatedList<UserQueryDto>> request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
