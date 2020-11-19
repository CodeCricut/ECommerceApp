using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.Common.Requests;
using ECommerceApp.Domain.Common.Models;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceApp.Application.ProductListingCQs.Queries.GetBySearch
{
	public class GetProductListingsBySearchQueryHandler : DatabaseRequestHandler<
		GetBySearchQuery<PaginatedList<ProductListingQueryDto>>,
		PaginatedList<ProductListingQueryDto>
		>
	{
		public GetProductListingsBySearchQueryHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override Task<PaginatedList<ProductListingQueryDto>> Handle(GetBySearchQuery<PaginatedList<ProductListingQueryDto>> request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
