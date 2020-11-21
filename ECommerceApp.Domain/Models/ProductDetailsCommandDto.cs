using AutoMapper;
using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Common.Mapping;
using ECommerceApp.Domain.Entities;
using System;

namespace ECommerceApp.Domain.Models
{
	public class ProductDetailsCommandDto : CommandDto, IMapFrom<ProductDetails>
	{
		public string HumanReadableId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Brand { get; set; }
		public decimal PricePerUnit { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<ProductDetailsCommandDto, ProductDetails>();
		}
	}
}
