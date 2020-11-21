using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.Common.Requests;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceApp.Application.UserCQs.Commands.ModifyUser
{
	public class ModifyUserCommand : IRequest<UserQueryDto>
	{
		public ModifyUserCommand(UserCommandDto updateModel)
		{
			UpdateModel = updateModel;
		}

		public UserCommandDto UpdateModel { get; }
	}

	public class ModifyUserHandler : DatabaseRequestHandler<
		ModifyUserCommand,
		UserQueryDto>
	{
		public ModifyUserHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override Task<UserQueryDto> Handle(ModifyUserCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
