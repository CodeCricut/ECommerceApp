using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.Common.Requests;
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

		public override Task<ProductListingQueryDto> Handle(RemoveProductListingCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
