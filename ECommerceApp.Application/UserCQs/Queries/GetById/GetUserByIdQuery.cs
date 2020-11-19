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

namespace ECommerceApp.Application.UserCQs.Queries.GetById
{
	public class GetUserByIdQueryHandler : DatabaseRequestHandler<
		GetByIdQuery<UserQueryDto>,
		UserQueryDto
		>
	{
		public GetUserByIdQueryHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override Task<UserQueryDto> Handle(GetByIdQuery<UserQueryDto> request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
