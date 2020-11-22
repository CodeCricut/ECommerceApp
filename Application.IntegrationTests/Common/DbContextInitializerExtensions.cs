
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Entities.JoinEntities;
using ECommerceApp.Persistance;
using System;
using System.Collections.Generic;

namespace Application.IntegrationTests
{
	public static class DbContextInitializerExtensions
	{,
		public static void InitializeForTests(this ECommerceContext context)
		{
			User user = new User
			{
				Bio = "bio1",
				Bought = new List<ProductDetails>(),
				Password = "password1",
				Products = new List<ProductListing>(),
				Saved = new List<UserProductListing_Saved>(),
				Deleted = false,
				ShoppingCartItems = new List<UserProductListing_Shopping>(),
				Sold = new List<ProductDetails>(),
				Username = "username1"
			};
			context.Users.Add(user);

			ProductListing productListing = new ProductListing
			{
				Available = true,
				Brand = "brand1",
				Description = "desc. 1",
				HumanReadableId = "hr id 1",
				ListedAt = DateTime.Now,
				Name = "name1",
				Price = 0.01M,
				QuantityAvailable = 10,
				Seller = user,
				SellerId = user.Id,
				UsersSaved = new List<UserProductListing_Saved>(),
				UsersShopping = new List<UserProductListing_Shopping>(),
			};
			context.ProductListings.Add(productListing);

			var productDetails = new ProductDetails
			{
				BoughtAt = DateTime.Now,
				Brand = "brand1",
				Description = "desc. 1",
				HumanReadableId = "hr id 1",
				ListedAt = DateTime.UtcNow.AddDays(-1),
				Name = "name1",
				PricePerUnit = 4.23M,
				QuantityBought = 2,
				Seller = user,
				SellerId = user.Id,
				User = user,
				UserId = user.Id
			};
			context.ProductDetails.Add(productDetails);

			context.SaveChanges();
		}

		public static void ClearDatabase(this ECommerceContext context)
		{
			context.Users.RemoveRange(context.Users);
			context.ProductListings.RemoveRange(context.ProductListings);
			context.ProductDetails.RemoveRange(context.ProductDetails);

			context.SaveChanges();
		}
	}
}
