using Application.IntegrationTests.Common;
using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.ProductDetailsCQs.Commands.BuyProduct;
using ECommerceApp.Application.ProductDetailsCQs.Commands.ReturnProduct;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Domain.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.ProductDetailsCQs.Commands
{
	public class ReturnProductTests : AppIntegrationTest
	{
		private User user;
		private ProductDetails productDetails;
		private ProductListing productListing;
		private int originalQuantityAvailable;
		private ProductDetailsQueryDto pdReturnedModel;

		public ReturnProductTests()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			user = (unitOfWork.Users.GetEntitiesAsync()).Result.First();
			productDetails = (unitOfWork.ProductDetails.GetEntitiesAsync()).Result.First();
			
			productListing = (unitOfWork.ProductListings.GetEntitiesAsync()).Result.First();
			originalQuantityAvailable = productListing.QuantityAvailable;

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(user.Id);

			Assert.Equal(productListing.SellerId, user.Id);
			Assert.Equal(productDetails.UserId, user.Id);

			var returnCommand = new ProductDetailsCommandDto
			{
				ProductListingId = productListing.Id
			};


			var sut = new ReturnProductHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			pdReturnedModel = sut.Handle(new ReturnProductCommand(returnCommand), new System.Threading.CancellationToken()).Result;
		}

		[Fact]
		public async Task ShouldRemoveProductDetails()
		{
			using var scope = Factory.Services.CreateScope();
			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

			Assert.Empty(pdReturnedModel.Errors);

			// Should remove product details from the db.
			var plRemovedEntity = await unitOfWork.ProductListings.GetEntityAsync(productDetails.Id);
			Assert.Null(plRemovedEntity);
		}

		[Fact]
		public async Task ShouldUpdateProductListing()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();
			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var updatedProductListing = (unitOfWork.ProductListings.GetEntitiesAsync()).Result.First();

			// Act
			// Should increment the product listing quantity available.
			Assert.Equal(originalQuantityAvailable + 1, updatedProductListing.QuantityAvailable);
		}

		[Fact]
		public async Task ShouldUpdateSeller()
		{
			using var scope = Factory.Services.CreateScope();
			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var seller = productListing.Seller;

			// Should remove the listing from Seller.Sold. 
			Assert.DoesNotContain(seller.Sold, pd => pd.Id == pdReturnedModel.Id);
		}

		[Fact]
		public async Task ShouldUpdateBuyer()
		{
			using var scope = Factory.Services.CreateScope();
			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var buyer = await unitOfWork.Users.GetEntityAsync(pdReturnedModel.UserId);

			// Should remove the details from Buyer.Bought
			Assert.DoesNotContain(buyer.Bought, pd => pd.Id == pdReturnedModel.Id);
		}
	}
}
