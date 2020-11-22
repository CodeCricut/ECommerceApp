using Application.IntegrationTests.Common;
using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.UserCQs.Queries.GetByUsername;
using ECommerceApp.Domain.Exceptions;
using ECommerceApp.Domain.Interfaces;
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
	public class GetByUsernameTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldReturnValidUser()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var user = (await unitOfWork.Users.GetEntitiesAsync()).First();

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(user.Id);

			var sut = new GetUserByUsernameHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			var query = new GetUserByUsernameQuery(user.Username);

			// Act
			var userResponse = await sut.Handle(query, new System.Threading.CancellationToken());

			// Assert
			Assert.NotNull(userResponse);
			Assert.Equal(user.Id, userResponse.Id);
			Assert.Empty(userResponse.Exceptions);
		}

		[Fact]
		public async Task ShouldNotReturnInvalidUser()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var user = (await unitOfWork.Users.GetEntitiesAsync()).First();

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(user.Id);

			var sut = new GetUserByUsernameHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			var query = new GetUserByUsernameQuery("invalid username");

			// Act
			var userResponse = await sut.Handle(query, new System.Threading.CancellationToken());

			// Assert
			Assert.NotNull(userResponse);

			var notFoundException = userResponse.Exceptions.FirstOrDefault(e => e is NotFoundException);
			Assert.NotNull(notFoundException);
		}
	}
}
