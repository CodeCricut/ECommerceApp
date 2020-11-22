using Application.IntegrationTests.Common;
using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.UserCQs.Queries.GetAuthenticatedUser;
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
	public class GetAuthenticatedUserTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldReturnLoggedInUser()
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

			var sut = new GetAuthenticatedUserHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			UserQueryDto authUserModel = await sut.Handle(new GetAuthenticatedUserQuery(), new System.Threading.CancellationToken());

			// Assert
			Assert.NotNull(authUserModel);

			Assert.Equal(user.Id, authUserModel.Id);
		}

		[Fact]
		public async Task ShouldThrowUnauthorizedNotLoggedIn()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(0);

			var sut = new GetAuthenticatedUserHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			var authUserModel = await sut.Handle(new GetAuthenticatedUserQuery(), new System.Threading.CancellationToken());

			// Assert
			var unauthorizedException = authUserModel.Exceptions.FirstOrDefault(e => e is UnauthorizedException);
			Assert.NotNull(unauthorizedException);
			Assert.True(authUserModel.Id <= 0);
			Assert.Equal(string.Empty, authUserModel.Username);
		}
	}
}
