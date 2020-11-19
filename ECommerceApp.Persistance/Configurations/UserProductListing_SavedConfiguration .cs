using ECommerceApp.Domain.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceApp.Persistance.Configurations
{
	class UserProductListing_SavedConfiguration : IEntityTypeConfiguration<UserProductListing_Saved>
	{
		public void Configure(EntityTypeBuilder<UserProductListing_Saved> builder)
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
		}
	}
}
