using ECommerceApp.Domain.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceApp.Persistance.Configurations
{
	class UserProductListingConfiguration : IEntityTypeConfiguration<UserProductListing>
	{
		public void Configure(EntityTypeBuilder<UserProductListing> builder)
		{
			builder.HasKey(upl => new { upl.UserId, upl.ProductListingId });
			
			builder
				.HasOne(upl => upl.User)
				.WithMany(u => u.Saved)
				.HasForeignKey(upl => upl.UserId)
				.OnDelete(DeleteBehavior.NoAction);
			builder
				.HasOne(upl => upl.ProductListing)
				.WithMany(listing => listing.UsersSaved)
				.HasForeignKey(upl => upl.ProductListingId)
				.OnDelete(DeleteBehavior.NoAction);

			// Todo: not sure if a different behavior should be set for shopping cart items.
			builder
				.HasOne(upl => upl.User)
				.WithMany(u => u.ShoppingCartItems)
				.HasForeignKey(upl => upl.UserId)
				.OnDelete(DeleteBehavior.NoAction);
			builder.HasOne(upl => upl.ProductListing)
				.WithMany(pl => pl.UsersShopping)
				.HasForeignKey(upl => upl.ProductListingId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
