using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Persistance.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApp.Persistance.Repositories
{
	public class UserRepository : EntityRepository<User>, IUserRepository
	{
		public UserRepository(DbContext context) : base(context)
		{
		}

		public override async Task<bool> DeleteEntityAsync(int id)
		{
			try
			{
				var entity = await _context.Set<User>().FindAsync(id);
				entity.Deleted = true;
				await UpdateEntityAsync(id, entity);

				return true;
			}
			catch
			{
				return false;
			}
		}

		public override Task<IQueryable<User>> GetEntitiesAsync()
		{
			return Task.FromResult(
				_context.Set<User>()
				.Include(u => u.Products)
				.Include(u => u.Saved)
				.Include(u => u.ShoppingCartItems)
				.Include(u => u.Sold)
				.AsQueryable()
				);
		}

		public override Task<User> GetEntityAsync(int id)
		{
			return Task.FromResult(
				_context.Set<User>()
				.Include(u => u.Products)
				.Include(u => u.Saved)
				.Include(u => u.ShoppingCartItems)
				.Include(u => u.Sold)
				.FirstOrDefault(u => u.Id == id)
				);
		}
	}
}
