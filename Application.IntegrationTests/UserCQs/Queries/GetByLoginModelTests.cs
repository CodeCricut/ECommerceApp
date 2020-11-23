using Application.IntegrationTests.Common;
using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.UserCQs.Queries.GetByIds;
using ECommerceApp.Application.UserCQs.Queries.GetUserFromLoginModel;
using ECommerceApp.Domain.Common.Models;
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
namespace Application.IntegrationTests.UserCQs.Queries
{
	public class GetByLoginModelTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldReturnValidUser()
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

			var sut = new GetUserFromLoginModelHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			var query = new GetUserFromLoginModelQuery(new UserCommandDto
			{
				Username = user.Username,
				Password = user.Password
			});

			// Act
			UserQueryDto authUserModel = await sut.Handle(query, new System.Threading.CancellationToken());

			// Assert
			Assert.NotNull(authUserModel);
			Assert.Empty(authUserModel.Errors);

			Assert.Equal(user.Bio, authUserModel.Bio);
			Assert.Equal(user.Bought.Count(), authUserModel.Bought.Count());
			Assert.Equal(user.Deleted, authUserModel.Deleted);
			Assert.Equal(user.Id, authUserModel.Id);
			Assert.Equal(user.Products.Count(), authUserModel.Products.Count());
			Assert.Equal(user.Saved.Count(), authUserModel.Saved.Count());
			Assert.Equal(user.ShoppingCartItems.Count(), authUserModel.ShoppingCartItems.Count());
			Assert.Equal(user.Sold.Count(), authUserModel.Sold.Count());
			Assert.Equal(user.Username.Count(), authUserModel.Username.Count());
		}

		[Fact]
		public async Task ShouldNotReturnInvalidUser()
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

			var sut = new GetUserFromLoginModelHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			var query = new GetUserFromLoginModelQuery(new UserCommandDto
			{
				Username = user.Username,
				Password = "INVALID PWD",
			});

			// Act
			UserQueryDto authUserModel = await sut.Handle(query, new System.Threading.CancellationToken());

			// Assert
			Assert.NotNull(authUserModel);

			var unauthorizedException = authUserModel.Errors.FirstOrDefault(e => e.GetType() == typeof( UnauthorizedException));
			Assert.NotNull(unauthorizedException);
		}
	}
}
