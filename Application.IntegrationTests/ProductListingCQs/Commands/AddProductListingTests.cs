using Application.IntegrationTests.Common;
using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.ProductListingCQs.Commands.AddProductListing;
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
	public class AddProductListingTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldCreateValidProductListing()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var user = (await unitOfWork.Users.GetEntitiesAsync()).First();
			//var productDetails = (await unitOfWork.ProductDetails.GetEntitiesAsync()).First();
			//var productListing = (await unitOfWork.ProductListings.GetEntitiesAsync()).First();

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(user.Id);

			var addCommand = new ProductListingCommandDto
			{
				Brand = "brand",
				Description = "desc",
				HumanReadableId = "hr id",
				Name = "name",
				Price = 42.23M,
				QuantityAvailable = 10
			};


			var sut = new AddProductListingHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			ProductListingQueryDto plAddedModel = await sut.Handle(new AddProductListingCommand(addCommand), new System.Threading.CancellationToken());

			// Assert
			Assert.NotNull(plAddedModel);
			Assert.Empty(plAddedModel.Errors);

			var plAddedEntity = await unitOfWork.ProductListings.GetEntityAsync(plAddedModel.Id);
			Assert.NotNull(plAddedEntity);

			Assert.Equal(addCommand.Brand, plAddedEntity.Brand);
			Assert.Equal(addCommand.Description, plAddedEntity.Description);
			Assert.Equal(addCommand.HumanReadableId, plAddedEntity.HumanReadableId);
			Assert.Equal(addCommand.Name, plAddedEntity.Name);
			Assert.Equal(addCommand.Price, plAddedEntity.Price);
			Assert.Equal(addCommand.QuantityAvailable, plAddedEntity.QuantityAvailable);

			Assert.Equal(user.Id, plAddedEntity.SellerId);
		}
	}
}
