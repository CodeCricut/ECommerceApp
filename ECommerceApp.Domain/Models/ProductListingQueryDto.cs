using AutoMapper;
using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Common.Mapping;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Entities.JoinEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceApp.Domain.Models
{
	public class ProductListingQueryDto : QueryDto, IMapFrom<ProductListing>
	{
		public int Id { get; set; }
		public string HumanReadableId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int SellerId { get; set; }
		public IEnumerable<int> UsersShopping { get; set; } = new List<int>();
		public IEnumerable<int> UsersSaved { get; set; } = new List<int>();
		public string Brand { get; set; }
		public decimal Price { get; set; }
		public DateTime ListedAt { get; set; }
		public bool Available { get; set; }
		public int QuantityAvailable { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<ProductListing, ProductListingQueryDto>()
				.ForMember(model => model.UsersShopping, u => u.MapFrom(u => u.UsersShopping.Select(us => us.UserId)))
				.ForMember(model => model.UsersSaved, u => u.MapFrom(u => u.UsersSaved.Select(us => us.UserId)));
		}
	}
}
