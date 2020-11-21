using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.Common.Requests;
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

		public override Task<UserQueryDto> Handle(RemoveCurrentUserCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
