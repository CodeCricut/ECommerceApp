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
		public string Username { get; set; }

		public IEnumerable<ProductListing> Products { get; set; } = new List<ProductListing>();
		public IEnumerable<UserProductListing_Shopping> ShoppingCartItems { get; set; } = new List<UserProductListing_Shopping>();
		public IEnumerable<UserProductListing_Saved> Saved { get; set; } = new List<UserProductListing_Saved>();
		public IEnumerable<ProductDetails> Bought { get; set; } = new List<ProductDetails>();
		public IEnumerable<ProductDetails> Sold { get; set; } = new List<ProductDetails>();
		public bool Deleted { get; set; }

		public void Mapping(Profile profile)
		{
			// TODO: add actual mapping behavior.
			profile.CreateMap<User, UserQueryDto>();
		}
	}
}
