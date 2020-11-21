using ECommerceApp.Domain.Common;
using System;

namespace ECommerceApp.Domain.Entities
{
	public class ProductDetails : DomainEntity
	{
		public string HumanReadableId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public User User { get; set; }
		public int UserId { get; set; }
		public User Seller { get; set; }
		public int SellerId { get; set; }
		public string Brand { get; set; }
		public decimal PricePerUnit { get; set; }
		public DateTime ListedAt { get; set; }
		public DateTime BoughtAt { get; set; }
		public int QuantityBought { get; set; }
	}
}
