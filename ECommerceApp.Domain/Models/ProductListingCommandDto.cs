using AutoMapper;
using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Common.Mapping;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Domain.Models
{
	public class ProductListingCommandDto : CommandDto, IMapFrom<ProductListing>
	{
		public void Mapping(Profile profile)
		{
			profile.CreateMap<ProductListingCommandDto, ProductListing>();
		}
	}
}
