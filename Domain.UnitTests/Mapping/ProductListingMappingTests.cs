using AutoMapper;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Entities.JoinEntities;
using ECommerceApp.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Domain.UnitTests.Mapping
{
	public class ProductListingMappingTests
	{
		private IServiceProvider _serviceProvider;

		public ProductListingMappingTests()
		{
			_serviceProvider = ServiceProviderFactory.CreateServiceProvider();
		}

		[Fact]
		public void ShouldMapFromCommandDto()
		{
			// Arrange
			using var scope = _serviceProvider.CreateScope();

			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var listingCommandDto = new ProductListingCommandDto
			{
				Brand = "test brand",
				Description = "test desc.",
				HumanReadableId = "test hr id",
				Name = "test name",
				Price = 3.23M,
				QuantityAvailable = 10
			};

			// Act
			var productListing = mapper.Map<ProductListing>(listingCommandDto);

			// Assert
			Assert.Equal(listingCommandDto.Brand, productListing.Brand);
			Assert.Equal(listingCommandDto.Description, productListing.Description);
			Assert.Equal(listingCommandDto.HumanReadableId, productListing.HumanReadableId);
			Assert.Equal(listingCommandDto.Name, productListing.Name);
			Assert.Equal(listingCommandDto.Price, productListing.Price);
			Assert.Equal(listingCommandDto.QuantityAvailable, productListing.QuantityAvailable);


			// TODO : add more robust tests for complex properties
			Assert.Empty(productListing.UsersSaved);
			Assert.Empty(productListing.UsersShopping);
		}

		[Fact]
		public void ShouldMapToQueryDto()
		{
			// Arrange
			using var scope = _serviceProvider.CreateScope();

			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var productListing = new ProductListing
			{
				Available = true,
				Id = 1,
				ListedAt = DateTime.Now,
				SellerId = 1,
				UsersSaved = new List<UserProductListing_Saved>(),
				UsersShopping = new List<UserProductListing_Shopping>(),
				Brand = "test brand",
				Description = "test desc.",
				HumanReadableId = "test hr id",
				Name = "test name",
				Price = 3.23M,
				QuantityAvailable = 10
			};

			// Act
			var listingQueryDto = mapper.Map<ProductListingQueryDto>(productListing);

			// Assert
			Assert.Equal(productListing.Available, listingQueryDto.Available);
			Assert.Equal(productListing.Id, listingQueryDto.Id);
			Assert.Equal(productListing.ListedAt, listingQueryDto.ListedAt);
			Assert.Equal(productListing.SellerId, listingQueryDto.SellerId);
			Assert.Equal(productListing.Brand, listingQueryDto.Brand);
			Assert.Equal(productListing.Description, listingQueryDto.Description);
			Assert.Equal(productListing.HumanReadableId, listingQueryDto.HumanReadableId);
			Assert.Equal(productListing.Name, listingQueryDto.Name);
			Assert.Equal(productListing.Price, listingQueryDto.Price);
			Assert.Equal(productListing.QuantityAvailable, listingQueryDto.QuantityAvailable);

			// TODO : add more robust tests for complex properties
			Assert.Empty(listingQueryDto.UsersSaved);
			Assert.Empty(listingQueryDto.UsersShopping);
		}
	}
}
