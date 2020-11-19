using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.Common.Requests;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
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

		public override Task<UserQueryDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
