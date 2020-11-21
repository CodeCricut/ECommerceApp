using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Entities.JoinEntities;
using System;
using System.Collections.Generic;

namespace ECommerceApp.Domain.Entities
{
	public class ProductListing : DomainEntity
	{
		public string HumanReadableId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public User Seller { get; set; }
		public int SellerId { get; set; }
		public IEnumerable<UserProductListing_Shopping> UsersShopping { get; set; } = new List<UserProductListing_Shopping>();
		public IEnumerable<UserProductListing_Saved> UsersSaved { get; set; } = new List<UserProductListing_Saved>();
		public string Brand { get; set; }
		public decimal Price { get; set; }
		public DateTime ListedAt { get; set; }
		public bool Available { get; set; }
		public int QuantityAvailable { get; set; }
	}
}
