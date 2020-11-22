using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

		public virtual async Task<IEnumerable<TEntity>> AddEntititesAsync(IEnumerable<TEntity> entities)
		{
			throw new System.NotImplementedException();
		}

		public virtual async Task<TEntity> AddEntityAsync(TEntity entity)
		{
			throw new System.NotImplementedException();
		}

		public virtual async Task<bool> DeleteEntityAsync(int id)
		{
			throw new System.NotImplementedException();
		}

		public virtual async Task<bool> EntityExistsAsync(int id)
		{
			throw new System.NotImplementedException();
		}

		public virtual async Task<IQueryable<TEntity>> GetEntitiesAsync()
		{
			throw new System.NotImplementedException();
		}

		public virtual async Task<TEntity> GetEntityAsync(int id)
		{
			throw new System.NotImplementedException();
		}

		public virtual async Task<bool> UpdateEntityAsync(int id, TEntity updatedEntity)
		{
			throw new System.NotImplementedException();
		}
	}
}
