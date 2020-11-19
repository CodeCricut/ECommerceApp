using ECommerceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.Persistance.Configurations
{
	public class ProductDetailsConfiguration : IEntityTypeConfiguration<ProductDetails>
	{
		public void Configure(EntityTypeBuilder<ProductDetails> builder)
		{
			builder.Property(pd => pd.BoughtAt).IsRequired();
			builder.Property(pd => pd.Description).IsRequired();
			builder.Property(pd => pd.HumanReadableId).IsRequired();
			builder.Property(pd => pd.Id).IsRequired();
			builder.Property(pd => pd.ListedAt).IsRequired();
			builder.Property(pd => pd.Name).IsRequired();
			builder.Property(pd => pd.PricePerUnit).IsRequired();
			builder.Property(pd => pd.QuantityBought).IsRequired();

			builder.Property(pd => pd.SellerId).IsRequired();
			builder
				.HasOne(pd => pd.Seller)
				.WithMany(seller => seller.Sold)
				.OnDelete(DeleteBehavior.NoAction);

			builder.Property(pd => pd.UserId).IsRequired();
			builder
				.HasOne(pd => pd.User)
				.WithMany(user => user.Bought)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
