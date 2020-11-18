using ECommerceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.Persistance.Configurations
{
	class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.Property(u => u.Bio).IsRequired();
			builder.Property(u => u.Deleted).IsRequired();
			builder.Property(u => u.Password).IsRequired();
			builder.Property(u => u.Username).IsRequired();
		}
	}
}
