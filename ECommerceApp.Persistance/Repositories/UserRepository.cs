using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Persistance.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Persistance.Repositories
{
	class UserRepository : EntityRepository<User>, IUserRepository
	{
		public UserRepository(DbContext context) : base(context)
		{
		}
	}
}
