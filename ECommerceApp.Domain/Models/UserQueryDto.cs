using AutoMapper;
using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Common.Mapping;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Entities.JoinEntities;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceApp.Domain.Models
{
	public class UserQueryDto : QueryDto, IMapFrom<User>
	{
		public int Id { get; set; }
		public string Bio { get; set; }
		public string Username { get; set; }

		public IEnumerable<int> Products { get; set; } = new List<int>();
		public IEnumerable<int> ShoppingCartItems { get; set; } = new List<int>();
		public IEnumerable<int> Saved { get; set; } = new List<int>();
		public IEnumerable<int> Bought { get; set; } = new List<int>();
		public IEnumerable<int> Sold { get; set; } = new List<int>();
		public bool Deleted { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<User, UserQueryDto>()
				.ForMember(model => model.Products, u => u.MapFrom(u => u.Products.Select(p => p.Id)))
				.ForMember(model => model.ShoppingCartItems, u => u.MapFrom(u => u.ShoppingCartItems.Select(sci => sci.ProductListingId)))
				.ForMember(model => model.Saved, u => u.MapFrom(u => u.Saved.Select(s => s.ProductListingId)))
				.ForMember(model => model.Bought, u => u.MapFrom(u => u.Bought.Select(b => b.Id)))
				.ForMember(model => model.Bought, u => u.MapFrom(u => u.Bought.Select(b => b.Id)))
				.ForMember(model => model.Sold, u => u.MapFrom(u => u.Sold.Select(b => b.Id)));
		}
	}
}
