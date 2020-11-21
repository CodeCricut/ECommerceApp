using AutoMapper;
using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Common.Mapping;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Domain.Models
{
	public class ProductListingQueryDto : QueryDto, IMapFrom<ProductListing>
	{
		public void Mapping(Profile profile)
		{
			// TODO: add actual mapping behavior.
			profile.CreateMap<ProductListing, ProductListingQueryDto>();
		}
	}
}
