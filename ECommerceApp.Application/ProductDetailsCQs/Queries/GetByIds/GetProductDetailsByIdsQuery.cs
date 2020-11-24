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

namespace ECommerceApp.Application.ProductDetailsCQs.Queries.GetByIds
{
	public class GetProductDetailsByIdsQuery : IRequest<PaginatedList<ProductDetailsQueryDto>>
	{
		public GetProductDetailsByIdsQuery(IEnumerable<int> ids, PagingParams pagingParams)
		{
			Ids = ids;
			PagingParams = pagingParams;
		}

		public IEnumerable<int> Ids { get; }
		public PagingParams PagingParams { get; }
	}

	public class GetProductDetailsByIdsHandler : DatabaseRequestHandler
		<GetProductDetailsByIdsQuery, PaginatedList<ProductDetailsQueryDto>>
	{
		public GetProductDetailsByIdsHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async  Task<PaginatedList<ProductDetailsQueryDto>> Handle(GetProductDetailsByIdsQuery request, CancellationToken cancellationToken)
		{
			var productDetails = await UnitOfWork.ProductDetails.GetEntitiesAsync();
			var paginatedDetails = await productDetails.Where(pd => request.Ids.Contains(pd.Id))
				.ToPaginatedListAsync(request.PagingParams);

			var paginatedDtoResponse = await paginatedDetails.ToMappedPaginatedListAsync<ProductDetails, ProductDetailsQueryDto>(Mapper);

			return paginatedDtoResponse;
		}
	}
}
