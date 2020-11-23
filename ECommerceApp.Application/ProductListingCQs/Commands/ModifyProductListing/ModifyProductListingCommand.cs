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

namespace ECommerceApp.Application.ProductListingCQs.Commands.ModifyProductListing
{
	public class ModifyProductListingCommand : IRequest<ProductListingQueryDto>
	{
		public ModifyProductListingCommand(int id, ProductListingCommandDto updateModel)
		{
			Id = id;
			UpdateModel = updateModel;
		}

		public int Id { get; }
		public ProductListingCommandDto UpdateModel { get; }
	}

	public class ModifyProductListingHandler : DatabaseRequestHandler<
		ModifyProductListingCommand,
		ProductListingQueryDto>
	{
		public ModifyProductListingHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<ProductListingQueryDto> Handle(ModifyProductListingCommand request, CancellationToken cancellationToken)
		{
			var response = new ProductListingQueryDto();
			var user = await UnitOfWork.Users.GetEntityAsync(CurrentUserService.UserId);
			var productListing = await UnitOfWork.ProductListings.GetEntityAsync(request.Id);

			if (user == null)
			{
				response.Errors.Add(new ErrorResponse(new UnauthorizedException()));
			}
			else if (productListing == null)
			{
				response.Errors.Add(new ErrorResponse(new NotFoundException()));
			}
			else
			{
				var updateModel = request.UpdateModel;
				productListing.Brand = updateModel.Brand;
				productListing.Description = updateModel.Description;
				productListing.Name = updateModel.Name;
				productListing.Price = updateModel.Price;
				productListing.QuantityAvailable = updateModel.QuantityAvailable;

				productListing.Available = productListing.QuantityAvailable > 0;

				var successful = await UnitOfWork.ProductListings.UpdateEntityAsync(request.Id, productListing);
				if (!successful)
				{
					response.Errors.Add(new ErrorResponse(new InvalidPostException()));
				} else
				{
					UnitOfWork.SaveChanges();
					response = Mapper.Map<ProductListingQueryDto>(productListing);
				}
			}

			return response;
		}
	}
}
