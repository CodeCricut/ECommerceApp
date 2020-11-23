using Application.IntegrationTests.Common;
using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
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

namespace Application.IntegrationTests.UserCQs.Commands
{
	public class RegisterUserTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldCreateValidUser()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			//var user = (await unitOfWork.Users.GetEntitiesAsync()).First();
			//var productDetails = (await unitOfWork.ProductDetails.GetEntitiesAsync()).First();
			//var productListing = (await unitOfWork.ProductListings.GetEntitiesAsync()).First();

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(0);

			var registerCommand = new UserCommandDto
			{
				Bio = "new bio",
				Password = "new password",
				Username = "new username"
			};

			var sut = new RegisterUserHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			UserQueryDto createdUserModel = await sut.Handle(new RegisterUserCommand(registerCommand), new System.Threading.CancellationToken());

			// Assert
			Assert.NotNull(createdUserModel);

			var createdUserEntity = await unitOfWork.Users.GetEntityAsync(createdUserModel.Id);

			Assert.NotNull(createdUserEntity);
			Assert.Equal(registerCommand.Bio, createdUserEntity.Bio);
			Assert.Equal(registerCommand.Password, createdUserEntity.Password);
			Assert.Equal(registerCommand.Username, createdUserEntity.Username);
		}

		[Fact]
		public async Task ShouldThrowUsernameTakenException()
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
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(0);

			var registerCommand = new UserCommandDto
			{
				Bio = "new bio",
				Password = "new password",
				Username = user.Username
			};

			var sut = new RegisterUserHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			UserQueryDto createdUserModel = await sut.Handle(new RegisterUserCommand(registerCommand), new System.Threading.CancellationToken());

			// Assert
			Assert.NotNull(createdUserModel);

			// TODO: check if the error is actually of type username taken
			Assert.True(0 < createdUserModel.Errors.Count());
		}
	}
}
