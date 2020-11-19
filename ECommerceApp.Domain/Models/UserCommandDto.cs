using AutoMapper;
using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Common.Mapping;
using ECommerceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
