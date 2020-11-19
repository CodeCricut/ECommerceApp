using ECommerceApp.Domain.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceApp.Persistance.Configurations
{
	class UserProductListing_ShoppingConfiguration : IEntityTypeConfiguration<UserProductListing_Shopping>
	{
		public void Configure(EntityTypeBuilder<UserProductListing_Shopping> builder)
		{
			builder.HasKey(upl => new { upl.UserId, upl.ProductListingId });
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

