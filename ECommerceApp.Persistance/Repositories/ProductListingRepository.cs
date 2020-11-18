using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Persistance.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.Persistance.Repositories
{
	class ProductListingRepository : EntityRepository<ProductListing>, IProductListingRepository
	{
		public ProductListingRepository(DbContext context) : base(context)
		{
		}
	}
}
