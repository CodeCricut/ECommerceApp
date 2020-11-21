using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.Common.Requests;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceApp.Application.ProductDetailsCQs.Queries.GetById
{
	public class GetProductDetailsByIdHandler : DatabaseRequestHandler<GetByIdQuery<ProductDetailsQueryDto>, ProductDetailsQueryDto>
	{
		public GetProductDetailsByIdHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override Task<ProductDetailsQueryDto> Handle(GetByIdQuery<ProductDetailsQueryDto> request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
