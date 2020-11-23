using Application.IntegrationTests.Common;
using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
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

namespace Application.IntegrationTests.UserCQs.Queries
{
	public class GetByIdTests : AppIntegrationTest
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

			var sut = new GetUserByIdHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			UserQueryDto authUserModel = await sut.Handle(new GetUserByIdQuery(user.Id), new System.Threading.CancellationToken());

			// Assert
			Assert.NotNull(authUserModel);

			Assert.Equal(user.Id, authUserModel.Id);

			//Assert.Empty(authUserModel.Bought);
			Assert.Empty(authUserModel.Errors);
			//Assert.Empty(authUserModel.Saved);
			//Assert.Empty(authUserModel.ShoppingCartItems);
			//Assert.Empty(authUserModel.Sold);
		}

		[Fact]
		public async Task ShouldNotReturnInvalidUser()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(-1);

			var sut = new GetUserByIdHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			UserQueryDto authUserModel = await sut.Handle(new GetUserByIdQuery(-1), new System.Threading.CancellationToken());

			// Assert
			Assert.True(0 < authUserModel.Errors.Count());
		}
	}
}
