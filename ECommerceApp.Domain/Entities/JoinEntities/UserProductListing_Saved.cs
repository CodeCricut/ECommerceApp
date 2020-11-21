namespace ECommerceApp.Domain.Entities.JoinEntities
{
	public class UserProductListing_Saved
	{
		public User User { get; set; }
		public int UserId { get; set; }
		public ProductListing ProductListing { get; set; }
		public int ProductListingId { get; set; }
	}
}
