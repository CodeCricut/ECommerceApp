using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.Common.Requests;
using ECommerceApp.Domain.Exceptions;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceApp.Application.UserCQs.Queries.GetById
{
	public class GetUserByIdQuery : IRequest<UserQueryDto>
	{
		public GetUserByIdQuery(int id)
		{
			Id = id;
		}

		public int Id { get; }
	}

	public class GetUserByIdHandler : DatabaseRequestHandler<
		GetUserByIdQuery,
		UserQueryDto
		>
	{
		public GetUserByIdHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<UserQueryDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
		{
			var user = await UnitOfWork.Users.GetEntityAsync(request.Id);

			UserQueryDto response = new UserQueryDto();
			if (user == null)
			{
				response.Errors = new List<ErrorResponse>
				{
					new ErrorResponse(new NotFoundException())
				};
			} else
			{
				if (user.Deleted)
				{
					return response;
				} else
				{
					// TODO: this should probably be dealt with elsewhere.
					response = Mapper.Map<UserQueryDto>(user);
					response.Bought = null;
					//response.Products = null;
					response.Saved = null;
					response.ShoppingCartItems = null;
					//response.Sold = null;
				}
			}
			return response;
		}
	}
}
