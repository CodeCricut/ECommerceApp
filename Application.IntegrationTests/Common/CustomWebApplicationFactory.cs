using ECommerceApp.Persistance;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;

namespace Application.IntegrationTests.Common
{
	public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.ConfigureAppConfiguration((context, configuration) =>
			{
				configuration.AddInMemoryCollection(
							new Dictionary<string, string>
							{
								["UseInMemoryDatabase"] = "true",
								// ["ConnectionStrings:ECommerce"] = "etskljldksjf"
							});
			});

			builder.ConfigureServices(services =>
			{
				// Configured to use InMemoryDB as per the Persistance project DI
				var sp = services.BuildServiceProvider();
				using (var scope = sp.CreateScope())
				{
					var scopedServices = scope.ServiceProvider;
					var db = (ECommerceContext)scopedServices.GetRequiredService<DbContext>();
					var logger = scopedServices
						.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

					db.Database.EnsureDeleted();
					db.Database.EnsureCreated();

					try
					{
						db.InitializeForTests();
					}
					catch (Exception ex)
					{
						logger.LogError(ex, "An error occurred seeding the " +
							"database with test messages. Error: {Message}", ex.Message);
					}
				}
			});
		}
	}
}
