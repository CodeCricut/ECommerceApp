using AutoMapper;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Domain.UnitTests.Mapping
{
	public class ProductDetailsMappingTests
	{
		private IServiceProvider _serviceProvider;

		public ProductDetailsMappingTests()
		{
			_serviceProvider = ServiceProviderFactory.CreateServiceProvider();
		}

		[Fact]
		public void ShouldMapFromCommandDto()
		{
			// Arrange
			using var scope = _serviceProvider.CreateScope();

			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var detailsCommandDto = new ProductDetailsCommandDto
			{
				Brand = "test brand",
				Description = "test description",
				Name = "test name",
				PricePerUnit = 4.20M
			};

			// Act
			var productDetails = mapper.Map<ProductDetails>(detailsCommandDto);

			// Assert
			Assert.Equal(detailsCommandDto.Brand, productDetails.Brand);
			Assert.Equal(detailsCommandDto.Description, productDetails.Description);
			Assert.Equal(detailsCommandDto.Name, productDetails.Name);
			Assert.Equal(detailsCommandDto.PricePerUnit, productDetails.PricePerUnit);


			// TODO : add more robust tests for complex properties
		}

		[Fact]
		public void ShouldMapToQueryDto()
		{
			// Arrange
			using var scope = _serviceProvider.CreateScope();

			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var productDetails = new ProductDetails
			{
				// BoughtAt = null,
				Brand = "test brand",
				Description = "test desc.",
				HumanReadableId = "test hr id",
				Id = 1,
				ListedAt = DateTime.Now,
				Name = "test name",
				PricePerUnit = 3.46M,
				QuantityBought = 0,
				SellerId = 2,
				UserId = 0
			};

			// Act
			var detailsQueryDto = mapper.Map<ProductDetailsQueryDto>(productDetails);

			// Assert
			Assert.Equal(productDetails.BoughtAt, detailsQueryDto.BoughtAt);
			Assert.Equal(productDetails.Brand, detailsQueryDto.Brand);
			Assert.Equal(productDetails.Description, detailsQueryDto.Description);
			Assert.Equal(productDetails.HumanReadableId, detailsQueryDto.HumanReadableId);
			Assert.Equal(productDetails.Id, detailsQueryDto.Id);
			Assert.Equal(productDetails.ListedAt, detailsQueryDto.ListedAt);
			Assert.Equal(productDetails.Name, detailsQueryDto.Name);
			Assert.Equal(productDetails.PricePerUnit, detailsQueryDto.PricePerUnit);
			Assert.Equal(productDetails.QuantityBought, detailsQueryDto.QuantityBought);
			Assert.Equal(productDetails.SellerId, detailsQueryDto.SellerId);
			Assert.Equal(productDetails.UserId, detailsQueryDto.UserId);
		}

	}
}
