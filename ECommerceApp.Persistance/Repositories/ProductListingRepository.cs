using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Persistance.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Persistance.Repositories
{
	class ProductListingRepository : EntityRepository<ProductListing>, IProductListingRepository
	{
		public ProductListingRepository(DbContext context) : base(context)
		{
		}
	}
}
