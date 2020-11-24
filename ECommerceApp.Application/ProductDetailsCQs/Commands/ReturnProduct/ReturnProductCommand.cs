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

namespace ECommerceApp.Application.ProductDetailsCQs.Commands.ReturnProduct
{
	public class ReturnProductCommand : IRequest<ProductDetailsQueryDto>
	{
		public ReturnProductCommand(int productDetailsId, ProductDetailsCommandDto returnCommand)
		{
			ProductDetailsId = productDetailsId;
			ReturnCommand = returnCommand;
		}

		public int ProductDetailsId { get; }
		public ProductDetailsCommandDto ReturnCommand { get; }
	}

	public class ReturnProductHandler : DatabaseRequestHandler<
		ReturnProductCommand,
		ProductDetailsQueryDto>
	{
		public ReturnProductHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<ProductDetailsQueryDto> Handle(ReturnProductCommand request, CancellationToken cancellationToken)
		{
			var user = await UnitOfWork.Users.GetEntityAsync(CurrentUserService.UserId);
			var productDetails = await UnitOfWork.ProductDetails.GetEntityAsync(request.ProductDetailsId);
			var productListing = await UnitOfWork.ProductListings.GetEntityAsync(request.ReturnCommand.ProductListingId);

			var response = new ProductDetailsQueryDto();

			if (user == null)
			{
				response.Errors.Add(new ErrorResponse(new UnauthorizedException()));
			}
			else if (productDetails == null)
			{
				response.Errors.Add(new ErrorResponse(new NotFoundException()));
			}
			else
			{
				// should increment the product listing number
				productListing.QuantityAvailable += request.ReturnCommand.QuantityBought;
				if (productListing.QuantityAvailable >= 0) productListing.Available = true;

				// should remove product details from the db

				var successful = await UnitOfWork.ProductDetails.DeleteEntityAsync(productDetails.Id);

				if (!successful)
				{
					response.Errors.Add(new ErrorResponse(new InvalidPostException()));
				} else
				{
					UnitOfWork.SaveChanges();
					response = Mapper.Map<ProductDetailsQueryDto>(productDetails);
				}
			}

			return response;
		}
	}
}
