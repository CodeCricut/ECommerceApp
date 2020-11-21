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

		public override Task<PaginatedList<ProductListingQueryDto>> Handle(GetProductListingsBySearchQuery request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
