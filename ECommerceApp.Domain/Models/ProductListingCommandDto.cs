using AutoMapper;
using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Common.Mapping;
using ECommerceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
