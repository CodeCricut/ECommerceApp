using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.Common.Requests;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Domain.Models;
using MediatR;
using System;
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

		public override Task<UserQueryDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
