using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.Common.Requests;
using ECommerceApp.Domain.Exceptions;
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
	}

	public class GetAuthenticatedUserHandler : DatabaseRequestHandler<
		GetAuthenticatedUserQuery,
		UserQueryDto>
	{
		public GetAuthenticatedUserHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<UserQueryDto> Handle(GetAuthenticatedUserQuery request, CancellationToken cancellationToken)
		{
			var currUser = await UnitOfWork.Users.GetEntityAsync(CurrentUserService.UserId);

			var response = new UserQueryDto();

			if (currUser == null)
			{
				response.Errors.Add(new ErrorResponse(new UnauthorizedException()));
			} else
			{
				response = Mapper.Map<UserQueryDto>(currUser);
			}
			return response;
		}
	}
}
