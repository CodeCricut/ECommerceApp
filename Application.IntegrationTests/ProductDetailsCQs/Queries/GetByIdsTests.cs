using Application.IntegrationTests.Common;
using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.ProductDetailsCQs.Queries.GetByIds;
using ECommerceApp.Application.ProductListingCQs.Queries.GetByIds;
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

namespace Application.IntegrationTests.ProductDetailsCQs.Queries
{
	public class GetByIdsTests : AppIntegrationTest
	{
		// TODO: should have multiple users in seed data to test with
		[Fact]
		public async Task ShouldReturnValidProductListings()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var user = (await unitOfWork.Users.GetEntitiesAsync()).First();
			var productDetails = (await unitOfWork.ProductDetails.GetEntitiesAsync()).First();

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(user.Id);

			var sut = new GetProductDetailsByIdsHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			var query = new GetProductDetailsByIdsQuery(new List<int> { productDetails.Id }, new PagingParams(1, 10));

			// Act
			PaginatedList<ProductDetailsQueryDto> paginatedReponse = await sut.Handle(query, new System.Threading.CancellationToken());

			// Assert
			Assert.NotNull(paginatedReponse);
			Assert.True(paginatedReponse.TotalCount > 0);
			Assert.Equal(1, paginatedReponse.PageIndex);

			var firstQueryDto = paginatedReponse.Items.FirstOrDefault();

			Assert.NotNull(firstQueryDto);
			Assert.Equal(productDetails.Id, firstQueryDto.Id);
		}

		[Fact]
		public async Task ShouldNotReturnInvalidListings()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();
			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var user = (await unitOfWork.Users.GetEntitiesAsync()).First();
			var productDetails = (await unitOfWork.ProductDetails.GetEntitiesAsync()).First();

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(user.Id);

			var sut = new GetProductDetailsByIdsHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			var query = new GetProductDetailsByIdsQuery(new List<int> { productDetails.Id }, new PagingParams(1, 10));

			// Act
			PaginatedList<ProductDetailsQueryDto> paginatedReponse = await sut.Handle(query, new System.Threading.CancellationToken());

			// Assert
			Assert.NotNull(paginatedReponse);
			Assert.Equal(0, paginatedReponse.TotalCount);
		}
	}
}
