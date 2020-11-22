using Application.IntegrationTests.Common;
using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.UserCQs.Commands.RemoveUser;
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
	public class RemoveUserTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldUpdateUser()
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

			var sut = new RemoveUserHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			UserQueryDto updatedUserModel = await sut.Handle(new RemoveCurrentUserCommand(), new System.Threading.CancellationToken());

			// Assert
			Assert.NotNull(updatedUserModel);

			var updatedUserEntity = await unitOfWork.Users.GetEntityAsync(user.Id);
			Assert.True(updatedUserEntity.Deleted);
		}
	}
}
