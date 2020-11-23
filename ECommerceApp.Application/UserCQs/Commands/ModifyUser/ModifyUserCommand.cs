using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.Common.Requests;
using ECommerceApp.Domain.Exceptions;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceApp.Application.UserCQs.Commands.ModifyUser
{
	public class ModifyUserCommand : IRequest<UserQueryDto>
	{
		public ModifyUserCommand(int id, UserCommandDto updateModel)
		{
			Id = id;
			UpdateModel = updateModel;
		}

		public int Id { get; }
		public UserCommandDto UpdateModel { get; }
	}

	public class ModifyUserHandler : DatabaseRequestHandler<
		ModifyUserCommand,
		UserQueryDto>
	{
		public ModifyUserHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<UserQueryDto> Handle(ModifyUserCommand request, CancellationToken cancellationToken)
		{
			var user = await UnitOfWork.Users.GetEntityAsync(request.Id);
			var response = new UserQueryDto();

			if (user == null)
			{
				response.Errors.Add(new ErrorResponse(new NotFoundException()));
			}
			else if (user.Id != CurrentUserService.UserId)
			{
				response.Errors.Add(new ErrorResponse(new UnauthorizedException()));
			} else
			{
				var updateModel = request.UpdateModel;
				user.Bio = updateModel.Bio;
				user.Password = updateModel.Password;
				// user.Username = updateModel.Username
				UnitOfWork.SaveChanges();
			}

			return Mapper.Map<UserQueryDto>(user);
		}
	}
}
