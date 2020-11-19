using AutoMapper;
using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Common.Mapping;
using ECommerceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.Domain.Models
{
	public class ProductDetailsQueryDto : QueryDto, IMapFrom<ProductDetails>
	{
		public void Mapping(Profile profile)
		{
			// TODO: add actual mapping behavior.
			profile.CreateMap<ProductDetails, ProductDetailsQueryDto>();
		}
	}
}
