using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.Domain.Interfaces
{
	public interface IUnitOfWork
	{
		IProductDetailsRepository ProductDetails { get; }
		IProductListingRepository ProductListings { get; }
		IUserRepository Users { get; }

		bool SaveChanges();
	}
}
