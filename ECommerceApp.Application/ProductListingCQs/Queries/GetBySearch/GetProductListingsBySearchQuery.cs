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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceApp.Application.ProductListingCQs.Queries.GetBySearch
{
	public class GetProductListingsBySearchQuery : IRequest<PaginatedList<ProductListingQueryDto>>
	{
		public GetProductListingsBySearchQuery(string searchTerm, PagingParams pagingParams)
		{
			SearchTerm = searchTerm;
			PagingParams = pagingParams;
		}

		public string SearchTerm { get; }
		public PagingParams PagingParams { get; }
	}

	public class GetProductListingsBySearchHandler : DatabaseRequestHandler<
		GetProductListingsBySearchQuery,
		PaginatedList<ProductListingQueryDto>
		>
	{
		public GetProductListingsBySearchHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<PaginatedList<ProductListingQueryDto>> Handle(GetProductListingsBySearchQuery request, CancellationToken cancellationToken)
		{
			var productListings = await UnitOfWork.ProductListings.GetEntitiesAsync();
			var paginatedListingsBySearch = await productListings.Where(pl => 
				pl.Brand.Contains(request.SearchTerm) ||
				pl.Description.Contains(request.SearchTerm) ||
				pl.Name.Contains(request.SearchTerm)
			)
				.ToPaginatedListAsync(request.PagingParams);

			var paginatedDtoResponse = await paginatedListingsBySearch.ToMappedPaginatedListAsync<ProductListing, ProductListingQueryDto>(Mapper);

			return paginatedDtoResponse;
		}
	}
}
