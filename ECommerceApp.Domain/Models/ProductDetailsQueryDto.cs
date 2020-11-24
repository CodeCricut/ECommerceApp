using AutoMapper;
using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Common.Mapping;
using ECommerceApp.Domain.Entities;
using System;

namespace ECommerceApp.Domain.Models
{
	public class ProductDetailsQueryDto : QueryDto, IMapFrom<ProductDetails>
	{
		public int Id { get; set; }
		public string HumanReadableId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int UserId { get; set; }
		public int SellerId { get; set; }
		public string Brand { get; set; }
		public decimal PricePerUnit { get; set; }
		public DateTime ListedAt { get; set; }
		public DateTime BoughtAt { get; set; }
		public int QuantityBought { get; set; }

		public int ProductListingId { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<ProductDetails, ProductDetailsQueryDto>();
		}
	}
}
