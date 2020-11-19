using ECommerceApp.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.Application.UserCQs.Queries.GetByUsername
{
	public class GetUserByUsernameQuery : IRequest<UserQueryDto>
	{
		public GetUserByUsernameQuery(string username)
		{
			Username = username;
		}

		public string Username { get; }
	}
}
