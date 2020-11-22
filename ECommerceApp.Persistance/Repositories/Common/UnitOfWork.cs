using ECommerceApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace ECommerceApp.Persistance.Repositories.Common
{
	class UnitOfWork : IUnitOfWork
	{
		private readonly DbContext _db;

		public IProductDetailsRepository ProductDetails { get; private set; }

		public IProductListingRepository ProductListings { get; private set; }

		public IUserRepository Users { get; private set; }

		public UnitOfWork(DbContext db)
		{
			_db = db;

			ProductDetails = new ProductDetailsRepository(db);
			ProductListings = new ProductListingRepository(db);
			Users = new UserRepository(db);
		}

		public bool SaveChanges()
		{
			return _db.SaveChanges() > 0;
		}

		public void Dispose()
		{
			_db.Dispose();
		}
	}
}
