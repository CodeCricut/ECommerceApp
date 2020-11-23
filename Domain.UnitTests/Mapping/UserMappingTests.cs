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
	public class UserMappingTests 
	{
		private IServiceProvider _serviceProvider;

		public UserMappingTests()
		{
			_serviceProvider = ServiceProviderFactory.CreateServiceProvider();
		}

		[Fact]
		public void ShouldMapFromCommandDto()
		{
			// Arrange
			using var scope = _serviceProvider.CreateScope();

			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var userCommandDto = new UserCommandDto
			{
				Bio = "test bio",
				Password = "test password",
				Username = "test username"
			};

			// Act
			var user = mapper.Map<User>(userCommandDto);

			// Assert
			Assert.Equal(userCommandDto.Bio, user.Bio);
			Assert.Equal(userCommandDto.Password, user.Password);
			Assert.Equal(userCommandDto.Username, user.Username);

			// TODO : add more robust tests for complex properties
			Assert.Empty(user.Bought);
			Assert.Empty(user.Products);
			Assert.Empty(user.Saved);
			Assert.Empty(user.ShoppingCartItems);
			Assert.Empty(user.Sold);
		}

		[Fact]
		public void ShouldMapToQueryDto()
		{
			// Arrange
			using var scope = _serviceProvider.CreateScope();

			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var user = new User
			{
				Bio = "test bio",
				Bought = new List<ProductDetails>(),
				Deleted = true,
				Id = 1,
				Password = "test password",
				Products = new List<ProductListing>(),
				Saved = new List<UserProductListing_Saved>(),
				ShoppingCartItems = new List<UserProductListing_Shopping>(),
				Sold = new List<ProductDetails>(),
				Username = "test username"
			};

			// Act
			var userQueryDto = mapper.Map<UserQueryDto>(user);

			// Assert
			Assert.Equal(user.Bio, userQueryDto.Bio);
			Assert.Equal(user.Deleted, userQueryDto.Deleted);
			Assert.Equal(user.Id, userQueryDto.Id);
			Assert.Equal(user.Username, userQueryDto.Username);

			Assert.Empty(userQueryDto.Errors);

			// TODO : add more robust tests for complex properties
			Assert.NotNull(userQueryDto.Bought);
			Assert.NotNull(userQueryDto.Products);
			Assert.NotNull(userQueryDto.Saved);
			Assert.NotNull(userQueryDto.ShoppingCartItems);
			Assert.NotNull(userQueryDto.Sold);
		}

		
	}
}
