using AutoMapper;
using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Common.Mapping;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Domain.Models
{
	public class UserCommandDto : CommandDto, IMapFrom<User>
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string Bio { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<UserCommandDto, User>();
		}
	}
}
