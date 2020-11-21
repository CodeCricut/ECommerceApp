using ECommerceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ECommerceApp.Persistance
{
	public class ECommerceContext : DbContext
	{
		public DbSet<ProductListing> ProductListings { get; set; }
		public DbSet<ProductDetails> ProductDetails { get; set; }
		public DbSet<User> Users { get; set; }

		// We will inject the configuration options in through the Api.Startup class.
		public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			base.OnModelCreating(builder);
		}
	}
}
