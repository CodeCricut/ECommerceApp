using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.Common.Requests;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceApp.Application.UserCQs.Queries.GetAuthenticatedUser
{
	public class GetAuthenticatedUserQuery : IRequest<UserQueryDto>
	{
		public GetAuthenticatedUserQuery(UserCommandDto loginModel)
		{
			LoginModel = loginModel;
		}

		public UserCommandDto LoginModel { get; }
	}

	public class GetAuthenticatedUserHandler : DatabaseRequestHandler<
		GetAuthenticatedUserQuery,
		UserQueryDto>
	{
		public GetAuthenticatedUserHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override Task<UserQueryDto> Handle(GetAuthenticatedUserQuery request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
