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

namespace ECommerceApp.Application.UserCQs.Commands.RemoveUser
{
	public class RemoveUserCommand : IRequest<UserQueryDto>
	{
		public RemoveUserCommand(int userId)
		{
			UserId = userId;
		}

		public int UserId { get; }
	}

	public class RemoveUserHandler : DatabaseRequestHandler<
		RemoveUserCommand,
		UserQueryDto>
	{
		public RemoveUserHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override Task<UserQueryDto> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
