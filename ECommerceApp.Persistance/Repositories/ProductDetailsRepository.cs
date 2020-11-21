using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Persistance.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Persistance.Repositories
{
	class ProductDetailsRepository : EntityRepository<ProductDetails>, IProductDetailsRepository
	{
		public ProductDetailsRepository(DbContext context) : base(context)
		{
		}
	}
}
