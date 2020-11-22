using Application.IntegrationTests.Common;
using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.UserCQs.Queries.GetByUsername;
using ECommerceApp.Application.UserCQs.Queries.GetByUsernames;
using ECommerceApp.Domain.Common.Models;
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
	public class GetByUsernamesTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldReturnValidUsers()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var user = (await unitOfWork.Users.GetEntitiesAsync()).First();

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(user.Id);

			var sut = new GetUsersByUsernamesHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			var query = new GetUsersByUsernamesQuery(new List<string> { user.Username }, new PagingParams(1, 10));

			// Act
			var userResponse = await sut.Handle(query, new System.Threading.CancellationToken());

			// Assert
			Assert.Equal(user.Id, userResponse.Items.FirstOrDefault().Id);
		}
	}
}
