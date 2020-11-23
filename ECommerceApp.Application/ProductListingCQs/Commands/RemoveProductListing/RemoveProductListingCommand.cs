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

namespace ECommerceApp.Application.ProductListingCQs.Commands.RemoveProductListing
{
	public class RemoveProductListingCommand : IRequest<ProductListingQueryDto>
	{
		public RemoveProductListingCommand(int productListingId)
		{
			ProductListingId = productListingId;
		}

		public int ProductListingId { get; }
	}

	public class RemoveProductListingHandler : DatabaseRequestHandler<
		RemoveProductListingCommand,
		ProductListingQueryDto>
	{
		public RemoveProductListingHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<ProductListingQueryDto> Handle(RemoveProductListingCommand request, CancellationToken cancellationToken)
		{
			var response = new ProductListingQueryDto();
			var user = await UnitOfWork.Users.GetEntityAsync(CurrentUserService.UserId);
			var productListing = await UnitOfWork.ProductListings.GetEntityAsync(request.ProductListingId);

			if (user == null)
			{
				response.Errors.Add(new ErrorResponse(new UnauthorizedException()));
			}
			else if (productListing == null)
			{
				response.Errors.Add(new ErrorResponse(new NotFoundException()));
			} else
			{
				var successful = await UnitOfWork.ProductListings.DeleteEntityAsync(request.ProductListingId);
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
