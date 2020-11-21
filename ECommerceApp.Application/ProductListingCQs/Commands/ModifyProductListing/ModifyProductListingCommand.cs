using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.Common.Requests;
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
		public ModifyProductListingCommand(ProductListingCommandDto updateModel)
		{
			UpdateModel = updateModel;
		}

		public ProductListingCommandDto UpdateModel { get; }
	}

	public class ModifyProductListingHandler : DatabaseRequestHandler<
		ModifyProductListingCommand,
		ProductListingQueryDto>
	{
		public ModifyProductListingHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override Task<ProductListingQueryDto> Handle(ModifyProductListingCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
