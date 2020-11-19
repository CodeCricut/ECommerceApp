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

namespace ECommerceApp.Application.ProductDetailsCQs.Commands.BuyProduct
{
	public class BuyProductCommand : IRequest<ProductDetailsQueryDto>
	{
		public BuyProductCommand(ProductDetailsCommandDto model)
		{
			Model = model;
		}

		public ProductDetailsCommandDto Model { get; }
	}

	public class BuyProductHandler : DatabaseRequestHandler<
		BuyProductCommand,
		ProductDetailsQueryDto>
	{
		public BuyProductHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override Task<ProductDetailsQueryDto> Handle(BuyProductCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
