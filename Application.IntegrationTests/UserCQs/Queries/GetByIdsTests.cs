using Application.IntegrationTests.Common;
using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.UserCQs.Queries.GetByIds;
using ECommerceApp.Domain.Common.Models;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Domain.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.UserCQs.Queries
{
	public class GetByIdsTests : AppIntegrationTest
	{
		// TODO: should have multiple users in seed data to test with
		[Fact]
		public async Task ShouldReturnValidUsers()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var user = (await unitOfWork.Users.GetEntitiesAsync()).First();

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(user.Id);

			var sut = new GetUsersByIdsHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			var query = new GetUsersByIdsQuery(new List<int> { 1 }, new PagingParams(1, 10));
			
			// Act
			PaginatedList<UserQueryDto> paginatedReponse = await sut.Handle(query, new System.Threading.CancellationToken());

			// Assert
			Assert.NotNull(paginatedReponse);
			Assert.True(paginatedReponse.TotalCount > 0);
			Assert.Equal(1, paginatedReponse.PageIndex);

			var firstQueryDto = paginatedReponse.Items.FirstOrDefault();

			Assert.NotNull(firstQueryDto);
			Assert.Equal(user.Id, firstQueryDto.Id);
		}

		[Fact]
		public async Task ShouldNotReturnInvalidUsers()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var user = (await unitOfWork.Users.GetEntitiesAsync()).First();

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(user.Id);

			var sut = new GetUsersByIdsHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			var query = new GetUsersByIdsQuery(new List<int> { -1 }, new PagingParams(1, 10));

			// Act
			PaginatedList<UserQueryDto> paginatedReponse = await sut.Handle(query, new System.Threading.CancellationToken());

			// Assert
			Assert.NotNull(paginatedReponse);
			Assert.Equal(0, paginatedReponse.TotalCount);
		}
	}
}
