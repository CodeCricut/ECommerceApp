using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.Common.Requests;
using ECommerceApp.Domain.Exceptions;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceApp.Application.ProductDetailsCQs.Queries.GetById
{
	public class GetProductDetailsByIdQuery : IRequest<ProductDetailsQueryDto>
	{
		public GetProductDetailsByIdQuery(int id)
		{
			Id = id;
		}

		public int Id { get; }
	}

	public class GetProductDetailsByIdHandler : DatabaseRequestHandler<GetProductDetailsByIdQuery, ProductDetailsQueryDto>
	{
		public GetProductDetailsByIdHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<ProductDetailsQueryDto> Handle(GetProductDetailsByIdQuery request, CancellationToken cancellationToken)
		{
			var productDetails = await UnitOfWork.ProductDetails.GetEntityAsync(request.Id);

			var response = new ProductDetailsQueryDto();

			if (productDetails == null)
			{
				response.Errors.Add(new ErrorResponse(new NotFoundException()));
			} else
			{
				response = Mapper.Map<ProductDetailsQueryDto>(productDetails);
			}

			return response;
		}
	}
}
