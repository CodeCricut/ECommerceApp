using ECommerceApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.Persistance.Repositories.Common
{
	class UnitOfWork : IUnitOfWork
	{
		public IProductDetailsRepository ProductDetails => throw new NotImplementedException();

		public IProductListingRepository ProductListings => throw new NotImplementedException();

		public IUserRepository Users => throw new NotImplementedException();

		public UnitOfWork(DbContext db)
		{

		}

		public bool SaveChanges()
		{
			throw new NotImplementedException();
		}
	}
}
