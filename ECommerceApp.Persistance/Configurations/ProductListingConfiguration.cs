using ECommerceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.Persistance.Configurations
{
	class ProductListingConfiguration : IEntityTypeConfiguration<ProductListing>
	{
		public void Configure(EntityTypeBuilder<ProductListing> builder)
		{
			builder.Property(pl => pl.Available).IsRequired();
			builder.Property(pl => pl.Brand).IsRequired();
			builder.Property(pl => pl.Description).IsRequired();
			builder.Property(pl => pl.HumanReadableId).IsRequired();
			builder.Property(pl => pl.ListedAt).IsRequired();
			builder.Property(pl => pl.Name).IsRequired();
			builder.Property(pl => pl.Price).IsRequired();
			builder.Property(pl => pl.QuantityAvailable).IsRequired();
			
			builder.Property(pl => pl.SellerId).IsRequired();
			builder.HasOne(pl => pl.Seller).WithMany(seller => seller.Products);
		}
	}
}
