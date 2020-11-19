using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.Common.Requests;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceApp.Application.ProductDetailsCQs.Commands.ReturnProduct
{
	public class ReturnProductCommand : IRequest<ProductDetailsQueryDto>
	{
		public ReturnProductCommand(int productListingId)
		{
			ProductListingId = productListingId;
		}

		public int ProductListingId { get; }
	}

	public class ReturnProductHandler : DatabaseRequestHandler<
		ReturnProductCommand,
		ProductDetailsQueryDto>
	{
		public ReturnProductHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override Task<ProductDetailsQueryDto> Handle(ReturnProductCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
