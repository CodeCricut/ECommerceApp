using Application.IntegrationTests.Common;
using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.ProductListingCQs.Commands.AddProductListing;
using ECommerceApp.Application.ProductListingCQs.Commands.ModifyProductListing;
using ECommerceApp.Application.ProductListingCQs.Commands.RemoveProductListing;
using ECommerceApp.Application.UserCQs.Commands.RegisterUser;
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

namespace Application.IntegrationTests.ProductListingCQs.Commands
{
	public class RemoveProductListingTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldRemoveValidProductListing()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var user = (await unitOfWork.Users.GetEntitiesAsync()).First();
			//var productDetails = (await unitOfWork.ProductDetails.GetEntitiesAsync()).First();
			var productListing = (await unitOfWork.ProductListings.GetEntitiesAsync()).First();

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(productListing.SellerId);


			var sut = new RemoveProductListingHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			ProductListingQueryDto plDeletedModel = await sut.Handle(new RemoveProductListingCommand(
				productListing.Id), new System.Threading.CancellationToken());

			// Assert
			Assert.NotNull(plDeletedModel);

			var deletedEntity = await unitOfWork.ProductListings.GetEntityAsync(plDeletedModel.Id);
			Assert.NotNull(deletedEntity);
			Assert.True(deletedEntity.Deleted);
		}

		[Fact]
		public async Task ShouldThrowUnauthorized()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var user = (await unitOfWork.Users.GetEntitiesAsync()).First();
			var productListing = (await unitOfWork.ProductListings.GetEntitiesAsync()).First();

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(-1);
			var sut = new RemoveProductListingHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			ProductListingQueryDto plDeletedModel = await sut.Handle(new RemoveProductListingCommand(
				productListing.Id), new System.Threading.CancellationToken());

			// Assert
			Assert.NotNull(plDeletedModel);

			var unauthorizedException = plDeletedModel.Exceptions.FirstOrDefault(e => e is UnauthorizedException);
			Assert.NotNull(unauthorizedException);

			Assert.False(productListing.Deleted);
		}
	}
}
