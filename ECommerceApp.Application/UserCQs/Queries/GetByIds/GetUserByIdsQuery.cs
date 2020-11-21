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

namespace ECommerceApp.Application.UserCQs.Queries.GetByIds
{
	public class GetUserByIdsQueryHandler : DatabaseRequestHandler<
		GetByIdQuery<PaginatedList<UserQueryDto>>,
		PaginatedList<UserQueryDto>
		>
	{
		public GetUserByIdsQueryHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override Task<PaginatedList<UserQueryDto>> Handle(GetByIdQuery<PaginatedList<UserQueryDto>> request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
