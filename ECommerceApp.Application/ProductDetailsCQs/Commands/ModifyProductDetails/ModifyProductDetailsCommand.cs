using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.Common.Requests;
using ECommerceApp.Domain.Exceptions;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Domain.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceApp.Application.ProductDetailsCQs.Commands.ModifyProductDetails
{
	public class ModifyProductDetailsCommand : IRequest<ProductDetailsQueryDto>
	{
		public ModifyProductDetailsCommand(int id, ProductDetailsCommandDto updateModel)
		{
			Id = id;
			UpdateModel = updateModel;
		}

		public int Id { get; }
		public ProductDetailsCommandDto UpdateModel { get; }
	}
	public class ModifyProductDetailsHandler : DatabaseRequestHandler<
		ModifyProductDetailsCommand,
		ProductDetailsQueryDto>
	{
		public ModifyProductDetailsHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<ProductDetailsQueryDto> Handle(ModifyProductDetailsCommand request, CancellationToken cancellationToken)
		{
			var user = await UnitOfWork.Users.GetEntityAsync(CurrentUserService.UserId);
			var productDetails = await UnitOfWork.ProductDetails.GetEntityAsync(request.Id);

			var response = new ProductDetailsQueryDto();

			if (user == null || ! user.Bought.Any(pd => pd.Id == request.Id))
			{
				response.Errors.Add(new ErrorResponse(new UnauthorizedException()));
			}
			else if (productDetails == null)
			{
				response.Errors.Add(new ErrorResponse(new NotFoundException()));
			}
			else
			{
				var updateModel = request.UpdateModel;
				productDetails.Brand = updateModel.Brand;
				productDetails.Description = updateModel.Description;
				productDetails.Name = updateModel.Name;

				var successful = await UnitOfWork.ProductDetails.UpdateEntityAsync(request.Id, productDetails);

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
