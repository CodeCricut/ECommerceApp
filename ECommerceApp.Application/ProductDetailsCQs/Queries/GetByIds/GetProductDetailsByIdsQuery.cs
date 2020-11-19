using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.Common.Requests;
using ECommerceApp.Domain.Common.Models;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceApp.Application.ProductDetailsCQs.Queries.GetByIds
{
	public class GetProductDetailsByIdsHandler : DatabaseRequestHandler
		<GetByIdsQuery<PaginatedList<ProductDetailsQueryDto>>, PaginatedList<ProductDetailsQueryDto>>
	{
		public GetProductDetailsByIdsHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override Task<PaginatedList<ProductDetailsQueryDto>> Handle(GetByIdsQuery<PaginatedList<ProductDetailsQueryDto>> request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
