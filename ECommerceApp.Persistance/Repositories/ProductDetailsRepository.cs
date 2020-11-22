using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Persistance.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApp.Persistance.Repositories
{
	class ProductDetailsRepository : EntityRepository<ProductDetails>, IProductDetailsRepository
	{
		public ProductDetailsRepository(DbContext context) : base(context)
		{
		}


		public override Task<IQueryable<ProductDetails>> GetEntitiesAsync()
		{
			return Task.FromResult(
				_context.Set<ProductDetails>()
				.Include(pd => pd.Seller)
				.Include(pd => pd.User)
				.AsQueryable()
				);
		}

		public override Task<ProductDetails> GetEntityAsync(int id)
		{
			return Task.FromResult(
				_context.Set<ProductDetails>()
				.Include(pd => pd.Seller)
				.Include(pd => pd.User)
				.FirstOrDefault(pd => pd.Id == id)
				);
		}
	}
}
