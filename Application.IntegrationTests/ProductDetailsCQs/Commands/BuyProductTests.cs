using Application.IntegrationTests.Common;
using AutoMapper;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.ProductDetailsCQs.Commands.BuyProduct;
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
	public class BuyProductTests : AppIntegrationTest
	{
		private User user;
		private ProductDetails productDetails;
		private ProductListing productListing;
		private int originalQuantityAvailable;
		private ProductDetailsQueryDto pdAddedModel;

		public BuyProductTests()
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

			var addCommand = new ProductDetailsCommandDto
			{
				ProductListingId = productListing.Id
			};


			var sut = new BuyProductHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			pdAddedModel = sut.Handle(new BuyProductCommand(addCommand), new System.Threading.CancellationToken()).Result;
		}

		[Fact]
		public async Task ShouldCreateProductDetails()
		{
			using var scope = Factory.Services.CreateScope();
			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

			// Should return details query dto.
			Assert.NotNull(pdAddedModel);
			Assert.Empty(pdAddedModel.Errors);

			// Should create new product details and add it to the db.
			var plAddedEntity = await unitOfWork.ProductDetails.GetEntityAsync(pdAddedModel.Id);
			Assert.NotNull(plAddedEntity);

			Assert.Equal(plAddedEntity.ProductListingId, productListing.Id);
		}

		[Fact]
		public async Task ShouldUpdateProductListing()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();
			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var updatedProductListing = (unitOfWork.ProductListings.GetEntitiesAsync()).Result.First();

			// Act
			// Should decrement the product listing number.
			Assert.Equal(originalQuantityAvailable - 1, updatedProductListing.QuantityAvailable);
		}

		[Fact]
		public async Task ShouldUpdateSeller()
		{
			using var scope = Factory.Services.CreateScope();
			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var seller = productListing.Seller;

			// Should add the listing to Seller.Sold. 
			Assert.Contains(seller.Sold, pd => pd.Id == pdAddedModel.Id);
		}

		[Fact]
		public async Task ShouldUpdateBuyer()
		{
			using var scope = Factory.Services.CreateScope();
			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var buyer = await unitOfWork.Users.GetEntityAsync(pdAddedModel.UserId);

			// Should add the details to Buyer.Bought
			Assert.Contains(buyer.Bought, pd => pd.Id == pdAddedModel.Id);
		}
	}
}
