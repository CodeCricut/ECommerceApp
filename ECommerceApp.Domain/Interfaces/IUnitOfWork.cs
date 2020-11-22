using System;

namespace ECommerceApp.Domain.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		IProductDetailsRepository ProductDetails { get; }
		IProductListingRepository ProductListings { get; }
		IUserRepository Users { get; }

		bool SaveChanges();
	}
}
