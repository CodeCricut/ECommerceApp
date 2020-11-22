using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.Common.Requests;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Domain.Models;
using MediatR;
using System;
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

		public override Task<ProductDetailsQueryDto> Handle(ModifyProductDetailsCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
