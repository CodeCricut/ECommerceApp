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

namespace ECommerceApp.Application.UserCQs.Queries.GetUserFromLoginModel
{
	public class GetUserFromLoginModelQuery : IRequest<UserQueryDto>
	{
		public GetUserFromLoginModelQuery(UserCommandDto loginModel)
		{
			LoginModel = loginModel;
		}

		public UserCommandDto LoginModel { get; }
	}

	public class GetUserFromLoginModelHandler : DatabaseRequestHandler<GetUserFromLoginModelQuery, UserQueryDto>
	{
		public GetUserFromLoginModelHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<UserQueryDto> Handle(GetUserFromLoginModelQuery request, CancellationToken cancellationToken)
		{
			var users = await UnitOfWork.Users.GetEntitiesAsync();
			var user = users.FirstOrDefault(u => u.Username == request.LoginModel.Username && u.Password == request.LoginModel.Password);

			var response = new UserQueryDto();
			if (user == null) response.Errors.Add(new ErrorResponse(new UnauthorizedException()));
			else
			{
				response = Mapper.Map<UserQueryDto>(user);
			}
			return response;
		}
	}
}
