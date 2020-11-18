using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Domain.Entities.JoinEntities
{
	public class UserProductListing
	{
		public User User { get; set; }
		public int UserId { get; set; }
		public ProductListing ProductListing { get; set; }
		public int ProductListingId { get; set; }
	}
}
