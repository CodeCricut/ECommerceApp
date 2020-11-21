using AutoMapper;
using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Common.Mapping;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Domain.Models
{
	public class UserCommandDto : CommandDto, IMapFrom<User>
	{
		public void Mapping(Profile profile)
		{
			profile.CreateMap<UserCommandDto, User>();
		}
	}
}
