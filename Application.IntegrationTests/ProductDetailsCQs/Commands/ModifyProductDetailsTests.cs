using Application.IntegrationTests.Common;
using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.ProductDetailsCQs.Commands.ModifyProductDetails;
using ECommerceApp.Application.ProductListingCQs.Commands.AddProductListing;
using ECommerceApp.Application.ProductListingCQs.Commands.ModifyProductListing;
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
namespace Application.IntegrationTests.ProductDetailsCQs.Commands
{
	public class ModifyProductDetailsTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldUpdateProductDetails()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var user = (await unitOfWork.Users.GetEntitiesAsync()).First();
			var productDetails = (await unitOfWork.ProductDetails.GetEntitiesAsync()).First();
			var productListing = (await unitOfWork.ProductListings.GetEntitiesAsync()).First();

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(user.Id);

			var updateCommand = new ProductDetailsCommandDto
			{
				Brand = "updated brand",
				Description = "updated desc",
				Name = "updated name",
			};

			var sut = new ModifyProductDetailsHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			ProductDetailsQueryDto pdUpdatedModel = await sut.Handle(new ModifyProductDetailsCommand(
				productDetails.Id, updateCommand), new System.Threading.CancellationToken());

			// Assert
			Assert.NotNull(pdUpdatedModel);
			Assert.Empty(pdUpdatedModel.Exceptions);

			var pdUpdatedEntity = await unitOfWork.ProductDetails.GetEntityAsync(pdUpdatedModel.Id);
			Assert.NotNull(pdUpdatedEntity);

			Assert.Equal(updateCommand.Brand, pdUpdatedEntity.Brand);
			Assert.Equal(updateCommand.Description, pdUpdatedEntity.Description);
			Assert.Equal(updateCommand.Name, pdUpdatedEntity.Name);
			Assert.Equal(user.Id, pdUpdatedEntity.SellerId);
		}
	}
}
