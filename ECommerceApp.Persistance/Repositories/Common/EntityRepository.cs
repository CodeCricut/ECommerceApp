using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Persistance.Repositories.Common
{
	public abstract class EntityRepository<TEntity> : IEntityRepository<TEntity>
		where TEntity : DomainEntity
	{
		private readonly DbContext _context;

		public EntityRepository(DbContext context)
		{
			_context = context;
		}
	}
}
