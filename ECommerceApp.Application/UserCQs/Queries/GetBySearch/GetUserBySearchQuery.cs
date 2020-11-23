using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.Common.Requests;
using ECommerceApp.Domain.Common.Mapping;
using ECommerceApp.Domain.Common.Models;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Domain.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceApp.Application.UserCQs.Queries.GetBySearch
{
	public class GetUsersBySearchQuery : IRequest<PaginatedList<UserQueryDto>>
	{
		public GetUsersBySearchQuery(string searchTerm, PagingParams pagingParams)
		{
			SearchTerm = searchTerm;
			PagingParams = pagingParams;
		}

		public string SearchTerm { get; }
		public PagingParams PagingParams { get; }
	}

	public class GetUsersBySearchHandler : DatabaseRequestHandler<
		GetUsersBySearchQuery,
		PaginatedList<UserQueryDto>
		>
	{
		public GetUsersBySearchHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<PaginatedList<UserQueryDto>> Handle(GetUsersBySearchQuery request, CancellationToken cancellationToken)
		{
			var users = await UnitOfWork.Users.GetEntitiesAsync();
			var usersBySearch = users.Where(u =>
				u.Username.Contains(request.SearchTerm) ||
				u.Bio.Contains(request.SearchTerm) ||
				u.Products.Any(p =>
					p.Brand.Contains(request.SearchTerm) ||
					p.Description.Contains(request.SearchTerm) ||
					p.Name.Contains(request.SearchTerm)
					)
				);

			var paginatedUsers = await usersBySearch.ToPaginatedListAsync(request.PagingParams);
			return await paginatedUsers.ToMappedPaginatedListAsync<User, UserQueryDto>(Mapper);
		}
	}
}
