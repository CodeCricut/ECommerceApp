using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.Common.Requests;
using ECommerceApp.Domain.Common.Models;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceApp.Application.ProductListingCQs.Queries.GetById.GetProductListingById
{
	public class GetProductListingByIdQuery : IRequest<ProductListingQueryDto>
	{
		public GetProductListingByIdQuery(int id)
		{
			Id = id;
		}

		public int Id { get; }
	}

	public class GetProductListingByIdHandler : DatabaseRequestHandler
		<GetProductListingByIdQuery, ProductListingQueryDto>
	{
		public GetProductListingByIdHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override Task<ProductListingQueryDto> Handle(GetProductListingByIdQuery request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
