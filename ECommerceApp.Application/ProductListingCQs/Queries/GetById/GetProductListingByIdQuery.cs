using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.Common.Requests;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceApp.Application.ProductListingCQs.Queries.GetById.GetProductListingById
{
	public class GetProductListingByIdHandler : DatabaseRequestHandler
		<GetByIdQuery<ProductListingQueryDto>, ProductListingQueryDto>
	{
		public GetProductListingByIdHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override Task<ProductListingQueryDto> Handle(GetByIdQuery<ProductListingQueryDto> request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
