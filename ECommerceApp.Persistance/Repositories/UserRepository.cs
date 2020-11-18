using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Persistance.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.Persistance.Repositories
{
	class UserRepository : EntityRepository<User>, IUserRepository
	{
	}
}
