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

namespace ECommerceApp.Application.UserCQs.Queries.GetByUsername
{
	public class GetUserByUsernameQuery : IRequest<UserQueryDto>
	{
		public GetUserByUsernameQuery(string username)
		{
			Username = username;
		}

		public string Username { get; }
	}

	public class GetUserByUsernameHandler : DatabaseRequestHandler<
		GetUserByUsernameQuery,
		UserQueryDto>
	{
		public GetUserByUsernameHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override Task<UserQueryDto> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
