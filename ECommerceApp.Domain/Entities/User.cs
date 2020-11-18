using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Entities.JoinEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Domain.Entities
{
	public class User : BaseUser
	{
		public string Bio { get; set; }
		public IEnumerable<ProductListing> Products { get; set; }
		public IEnumerable<UserProductListing_Shopping> ShoppingCartItems { get; set; }
		public IEnumerable<UserProductListing_Saved> Saved { get; set; }
		public IEnumerable<ProductDetails> Bought { get; set; }
		public IEnumerable<ProductDetails> Sold { get; set; }
		public bool Deleted { get; set; }
	}
}
