using Application.IntegrationTests.Common;
using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
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
namespace Application.IntegrationTests.ProductListingCQs.Commands
{
	public class ModifyProductListingTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldCreateValidProductListing()
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
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(user.Id);

			var updateCommand = new ProductListingCommandDto
			{
				Brand = "updated brand",
				Description = "updated desc",
				HumanReadableId = "updated hr id",
				Name = "updated name",
				Price = 23.33M,
				QuantityAvailable = 8
			};


			var sut = new ModifyProductListingHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			ProductListingQueryDto plUpdatedModel = await sut.Handle(new ModifyProductListingCommand(
				productListing.Id, updateCommand), new System.Threading.CancellationToken());

			// Assert
			Assert.NotNull(plUpdatedModel);
			Assert.Empty(plUpdatedModel.Errors);

			var plUpdatedEntity = await unitOfWork.ProductListings.GetEntityAsync(plUpdatedModel.Id);
			Assert.NotNull(plUpdatedEntity);

			Assert.Equal(updateCommand.Brand, plUpdatedEntity.Brand);
			Assert.Equal(updateCommand.Description, plUpdatedEntity.Description);
			Assert.Equal(updateCommand.HumanReadableId, plUpdatedEntity.HumanReadableId);
			Assert.Equal(updateCommand.Name, plUpdatedEntity.Name);
			Assert.Equal(updateCommand.Price, plUpdatedEntity.Price);
			Assert.Equal(updateCommand.QuantityAvailable, plUpdatedEntity.QuantityAvailable);

			Assert.Equal(user.Id, plUpdatedEntity.SellerId);
		}
	}
}
