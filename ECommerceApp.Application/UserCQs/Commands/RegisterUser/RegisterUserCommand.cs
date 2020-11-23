using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.Common.Requests;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Exceptions;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceApp.Application.UserCQs.Commands.RegisterUser
{
	public class RegisterUserCommand : IRequest<UserQueryDto>
	{
		public RegisterUserCommand(UserCommandDto model)
		{
			Model = model;
		}

		public UserCommandDto Model { get; }
	}

	public class RegisterUserHandler : DatabaseRequestHandler<
		RegisterUserCommand,
		UserQueryDto>
	{
		public RegisterUserHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<UserQueryDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
		{
			var user = Mapper.Map<User>(request.Model);

			// Verify username isn't taken
			var users = await UnitOfWork.Users.GetEntitiesAsync();
			var userWithUsername = users.FirstOrDefault(u => u.Username == user.Username);
			if (userWithUsername != null) return new UserQueryDto
			{
				Errors = new List<ErrorResponse> { new ErrorResponse(new UsernameTakenException()) }
			};

			var registeredUser = await UnitOfWork.Users.AddEntityAsync(user);

			UnitOfWork.SaveChanges();

			return Mapper.Map<UserQueryDto>(registeredUser);
		}
	}
}
