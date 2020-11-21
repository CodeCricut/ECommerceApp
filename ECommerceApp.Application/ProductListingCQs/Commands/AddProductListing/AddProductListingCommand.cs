using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.Common.Requests;
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

		public override Task<ProductListingQueryDto> Handle(AddProductListingCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
