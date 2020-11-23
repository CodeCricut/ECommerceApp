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

namespace ECommerceApp.Application.ProductListingCQs.Commands.AddProductListing
{
	public class AddProductListingCommand : IRequest<ProductListingQueryDto>
	{
		public AddProductListingCommand(ProductListingCommandDto model)
		{
			Model = model;
		}

		public ProductListingCommandDto Model { get; }
	}

	public class AddProductListingHandler : DatabaseRequestHandler<
		AddProductListingCommand,
		ProductListingQueryDto>
	{
		public AddProductListingHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<ProductListingQueryDto> Handle(AddProductListingCommand request, CancellationToken cancellationToken)
		{
			var response = new ProductListingQueryDto();
			var user = await UnitOfWork.Users.GetEntityAsync(CurrentUserService.UserId);
			if (user == null)
			{
				response.Errors.Add(new ErrorResponse(new UnauthorizedException()));
			} else
			{
				var productListing = Mapper.Map<ProductListing>(request.Model);
				productListing.Available = productListing.QuantityAvailable > 0;
				productListing.Deleted = false;
				productListing.ListedAt = DateTime.Now;
				productListing.SellerId = user.Id;

				await UnitOfWork.ProductListings.AddEntityAsync(productListing);
				UnitOfWork.SaveChanges();

				response = Mapper.Map<ProductListingQueryDto>(productListing);
			}

			return response;
		}
	}
}
