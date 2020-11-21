using AutoMapper;
using ECommerceApp.Domain.Common.Mapping;
using ECommerceApp.Domain.Common.Models;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Domain.UnitTests.Mapping
{
	public class MappingExtensionsTests
	{
		private IServiceProvider _serviceProvider;

		public MappingExtensionsTests()
		{
			_serviceProvider = ServiceProviderFactory.CreateServiceProvider();
		}

		[Fact]
		public async Task ShouldMapToPaginatedList()
		{
			// Arrange
			var users = new List<User>
			{
				new User
				{
					Id = 1
				},
				new User
				{
					Id = 2
				},
				new User
				{
					Id = 3
				}
			};

			var queryableUsers = users.AsQueryable();

			// Act
			var paginatedUsers1 = await queryableUsers.ToPaginatedListAsync(new PagingParams(pageNumber: 1, pageSize: 1));
			var paginatedUsers2 = await queryableUsers.ToPaginatedListAsync(new PagingParams(pageNumber: 2, pageSize: 1));
			var paginatedUsers3 = await queryableUsers.ToPaginatedListAsync(new PagingParams(pageNumber: 3, pageSize: 1));


			// Assert
			Assert.NotEmpty(paginatedUsers1.Items);
			Assert.False(paginatedUsers1.HasPreviousPage);
			Assert.True(paginatedUsers1.HasNextPage);

			Assert.Equal(1, paginatedUsers1.PageIndex);
			Assert.Equal(1, paginatedUsers1.PageSize);

			Assert.Equal(3, paginatedUsers1.TotalCount);
			Assert.Equal(3, paginatedUsers1.TotalPages);

			Assert.Single(paginatedUsers2.Items);
			Assert.True(paginatedUsers2.HasPreviousPage);
			Assert.True(paginatedUsers2.HasNextPage);

			Assert.True(paginatedUsers3.HasPreviousPage);
			Assert.False(paginatedUsers3.HasNextPage);
		}

		[Fact]
		public async Task ShouldMapPaginatedListType()
		{
			// Arrange
			using var scope = _serviceProvider.CreateScope();

			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var users = new List<User>
			{
				new User
				{
					Id = 1
				},
				new User
				{
					Id = 2
				},
				new User
				{
					Id = 3
				}
			};
			var paginatedUsers = await users.AsQueryable().ToPaginatedListAsync(new PagingParams(1, 3));

			// Act
			var paginatedUserQueryDtos = await paginatedUsers.ToMappedPaginatedListAsync<User, UserQueryDto>(mapper);

			// Assert
			Assert.NotNull(paginatedUserQueryDtos.Items);
			Assert.Equal(3, paginatedUserQueryDtos.TotalCount);
			
			foreach(var userQueryDto in paginatedUserQueryDtos.Items)
				Assert.True(userQueryDto.Id > 0);
		}
	}
}
