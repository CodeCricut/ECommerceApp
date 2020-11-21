using AutoMapper;
using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Common.Mapping;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Domain.Models
{
	public class ProductListingCommandDto : CommandDto, IMapFrom<ProductListing>
	{
		public string HumanReadableId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Brand { get; set; }
		public decimal Price { get; set; }
		public int QuantityAvailable { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<ProductListingCommandDto, ProductListing>();
		}
	}
}
