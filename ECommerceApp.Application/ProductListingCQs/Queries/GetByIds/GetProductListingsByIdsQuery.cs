using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.Common.Requests;
using ECommerceApp.Domain.Common.Mapping;
using ECommerceApp.Domain.Common.Models;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceApp.Application.ProductListingCQs.Queries.GetByIds
{

	public class GetProductListingsByIdsQuery : IRequest<PaginatedList<ProductListingQueryDto>>
	{
		public GetProductListingsByIdsQuery(IEnumerable<int> ids, PagingParams pagingParams)
		{
			Ids = ids;
			PagingParams = pagingParams;
		}

		public IEnumerable<int> Ids { get; }
		public PagingParams PagingParams { get; }
	}


	public class GetProductListingsByIdsHandler : DatabaseRequestHandler
		<GetProductListingsByIdsQuery, PaginatedList<ProductListingQueryDto>>
	{
		public GetProductListingsByIdsHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<PaginatedList<ProductListingQueryDto>> Handle(GetProductListingsByIdsQuery request, CancellationToken cancellationToken)
		{
			var productListings = await UnitOfWork.ProductListings.GetEntitiesAsync();
			var paginatedListingsByIds = await productListings.Where(pl => request.Ids.Contains(pl.Id))
				.ToPaginatedListAsync(request.PagingParams);

			var paginatedDtoResponse = await paginatedListingsByIds.ToMappedPaginatedListAsync<ProductListing, ProductListingQueryDto>(Mapper);

			return paginatedDtoResponse;
		}
	}
}
