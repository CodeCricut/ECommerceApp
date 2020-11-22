using Application.IntegrationTests.Common;
using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.ProductDetailsCQs.Queries.GetById;
using ECommerceApp.Application.ProductListingCQs.Queries.GetById.GetProductListingById;
using ECommerceApp.Application.UserCQs.Queries.GetById;
using ECommerceApp.Domain.Exceptions;
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
	public class GetByIdTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldReturnValidProductDetails()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var user = (await unitOfWork.Users.GetEntitiesAsync()).First();
			var productDetails = (await unitOfWork.ProductDetails.GetEntitiesAsync()).First();
			//var productListing = (await unitOfWork.ProductListings.GetEntitiesAsync()).First();

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(user.Id);

			var sut = new GetProductDetailsByIdHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			ProductDetailsQueryDto responseModel = await sut.Handle(new GetProductDetailsByIdQuery(productDetails.Id), new System.Threading.CancellationToken());

			// Assert
			Assert.NotNull(responseModel);

			Assert.Equal(productDetails.Id, responseModel.Id);
			Assert.Empty(responseModel.Exceptions);
		}

		[Fact]
		public async Task ShouldNotReturnInvalidProductListing()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var user = (await unitOfWork.Users.GetEntitiesAsync()).First();
			var productDetails = (await unitOfWork.ProductDetails.GetEntitiesAsync()).First();
			//var productListing = (await unitOfWork.ProductListings.GetEntitiesAsync()).First();

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(user.Id);

			var sut = new GetProductDetailsByIdHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			ProductDetailsQueryDto responseModel = await sut.Handle(new GetProductDetailsByIdQuery(productDetails.Id), new System.Threading.CancellationToken());

			// Assert
			Assert.NotNull(responseModel);

			Assert.NotEqual(productDetails.Id, responseModel.Id);

			var notFoundException = responseModel.Exceptions.FirstOrDefault(e => e is NotFoundException);
			Assert.NotNull(notFoundException);
		}
	}
}
