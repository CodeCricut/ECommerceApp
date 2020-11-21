using AutoMapper;
using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Common.Mapping;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Entities.JoinEntities;
using System.Collections.Generic;

namespace ECommerceApp.Domain.Models
{
	public class UserQueryDto : QueryDto, IMapFrom<User>
	{
		public int Id { get; set; }
		public string Bio { get; set; }
		public IEnumerable<ProductListing> Products { get; set; }
		public IEnumerable<UserProductListing_Shopping> ShoppingCartItems { get; set; }
		public IEnumerable<UserProductListing_Saved> Saved { get; set; }
		public IEnumerable<ProductDetails> Bought { get; set; }
		public IEnumerable<ProductDetails> Sold { get; set; }
		public bool Deleted { get; set; }

		public void Mapping(Profile profile)
		{
			// TODO: add actual mapping behavior.
			profile.CreateMap<User, UserQueryDto>();
		}
	}
}
