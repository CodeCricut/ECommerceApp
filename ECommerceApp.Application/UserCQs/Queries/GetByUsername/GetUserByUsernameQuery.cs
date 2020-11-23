using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.Common.Requests;
using ECommerceApp.Domain.Exceptions;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Domain.Models;
using MediatR;
using System;
using System.Linq;
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

		public override async Task<UserQueryDto> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
		{
			var users = await UnitOfWork.Users.GetEntitiesAsync();
			var user = users.FirstOrDefault(u => u.Username == request.Username);

			var response = new UserQueryDto();

			if (user == null)
			{
				response.Errors.Add(new ErrorResponse(new NotFoundException()));
			} else
			{
				response = Mapper.Map<UserQueryDto>(user);
			}
			return response;
		}
	}
}
