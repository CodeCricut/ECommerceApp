using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.Common.Requests;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Exceptions;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceApp.Application.ProductDetailsCQs.Commands.BuyProduct
{
	public class BuyProductCommand : IRequest<ProductDetailsQueryDto>
	{
		public BuyProductCommand(ProductDetailsCommandDto model)
		{
			ProductDetailsModel = model;
		}

		public ProductDetailsCommandDto ProductDetailsModel { get; }
	}

	public class BuyProductHandler : DatabaseRequestHandler<
		BuyProductCommand,
		ProductDetailsQueryDto>
	{
		public BuyProductHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<ProductDetailsQueryDto> Handle(BuyProductCommand request, CancellationToken cancellationToken)
		{
			var user = await UnitOfWork.Users.GetEntityAsync(CurrentUserService.UserId);
			var productListing = await UnitOfWork.ProductListings.GetEntityAsync(request.ProductDetailsModel.ProductListingId);

			var response = new ProductDetailsQueryDto();

			if (user == null)
			{
				response.Errors.Add(new ErrorResponse(new UnauthorizedException()));
			} else if (productListing == null)
			{
				response.Errors.Add(new ErrorResponse(new NotFoundException()));
			} else if (! productListing.Available || productListing.QuantityAvailable <= 0)
			{
				response.Errors.Add(new ErrorResponse(new ProductUnavailableException()));
			} else
			{
				// should decrement the product listing number
				productListing.QuantityAvailable -= request.ProductDetailsModel.QuantityBought;
				if (productListing.QuantityAvailable <= 0) productListing.Available = false;

				// should create new product details and add it to the db
				var productDetails = new ProductDetails
				{
					ProductListingId = productListing.Id,
					BoughtAt = DateTime.Now,
					Brand = productListing.Brand,
					Description = productListing.Description,
					HumanReadableId = productListing.HumanReadableId,
					ListedAt = productListing.ListedAt,
					Name = productListing.Name,
					PricePerUnit = productListing.Price,
					QuantityBought = request.ProductDetailsModel.QuantityBought,
					SellerId = productListing.SellerId,
					UserId = user.Id
				};

				var addedEntity = await UnitOfWork.ProductDetails.AddEntityAsync(productDetails);
				UnitOfWork.SaveChanges();

				// should return details query dto
				response = Mapper.Map<ProductDetailsQueryDto>(addedEntity);
			}

			return response;
		}
	}
}
