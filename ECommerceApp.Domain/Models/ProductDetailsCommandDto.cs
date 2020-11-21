using AutoMapper;
using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Common.Mapping;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Domain.Models
{
	public class ProductDetailsCommandDto : CommandDto, IMapFrom<ProductDetails>
	{
		public void Mapping(Profile profile)
		{
			profile.CreateMap<ProductDetailsCommandDto, ProductDetails>();
		}
	}
}
