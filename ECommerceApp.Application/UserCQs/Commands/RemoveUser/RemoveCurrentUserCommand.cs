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

namespace ECommerceApp.Application.UserCQs.Commands.RemoveUser
{
	public class RemoveCurrentUserCommand : IRequest<UserQueryDto>
	{
	}

	public class RemoveUserHandler : DatabaseRequestHandler<
		RemoveCurrentUserCommand,
		UserQueryDto>
	{
		public RemoveUserHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<UserQueryDto> Handle(RemoveCurrentUserCommand request, CancellationToken cancellationToken)
		{
			var user = await UnitOfWork.Users.GetEntityAsync(CurrentUserService.UserId);

			var response = new UserQueryDto();
			
			if (user == null)
			{
				response.Errors.Add(new ErrorResponse(new UnauthorizedException()));
			} else
			{
				try
				{
					await UnitOfWork.Users.DeleteEntityAsync(user.Id);
					UnitOfWork.SaveChanges();
					response = Mapper.Map<UserQueryDto>(user);
				}
				catch (Exception e)
				{
					response.Errors.Add(new ErrorResponse(e));
				}
			}
			return response;
		}
	}
}
