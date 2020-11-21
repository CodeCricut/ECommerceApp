using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceApp.Application.Common.Requests
{
	public abstract class DatabaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
	{
		// Properties which are requeired for almost all database requests.
		public IUnitOfWork UnitOfWork { get; set; }
		public IMediator Mediator { get; set; }
		public IMapper Mapper { get; set; }
		public ICurrentUserService CurrentUserService { get; set; }

		public DatabaseRequestHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService)
		{
			UnitOfWork = unitOfWork;
			Mediator = mediator;
			Mapper = mapper;
			CurrentUserService = currentUserService;
		}

		public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
	}
}
