using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Persistance.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApp.Persistance.Repositories
{
	public class ProductListingRepository : EntityRepository<ProductListing>, IProductListingRepository
	{
		public ProductListingRepository(DbContext context) : base(context)
		{
		}


		public override Task<IQueryable<ProductListing>> GetEntitiesAsync()
		{
			return Task.FromResult(
				_context.Set<ProductListing>()
				.Include(pl => pl.Seller)
				.Include(pl => pl.UsersSaved)
				.Include(pl => pl.UsersShopping)
				.AsQueryable()
				);
		}

		public override Task<ProductListing> GetEntityAsync(int id)
		{
			return Task.FromResult(
				_context.Set<ProductListing>()
				.Include(pl => pl.Seller)
				.Include(pl => pl.UsersSaved)
				.Include(pl => pl.UsersShopping)
				.FirstOrDefault(pl => pl.Id == id)
				);
		}
	}
}
