using ECommerceApp.Persistance;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Application.IntegrationTests.Common
{
	public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			var projectDir = Directory.GetCurrentDirectory();
			var configPath = Path.Combine(projectDir, "appsettings.json");

			builder.ConfigureAppConfiguration(config =>
			{
				var integrationConfig = new ConfigurationBuilder()
					.AddJsonFile(configPath)
					.Build();

				config.AddConfiguration(integrationConfig);
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
