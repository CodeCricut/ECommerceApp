using ECommerceApp.Domain.Interfaces;
using ECommerceApp.Persistance.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ECommerceApp.Persistance
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
		{
			if (configuration.GetValue<bool>("UseInMemoryDatabase"))
			{
				// In order to run tests in parallel without issues, DB name must be unique.
				var inMemoryDbName = $"ECommerceDbMemory.{Guid.NewGuid()}";

				services.AddDbContext<DbContext, ECommerceContext>(options =>
					options.UseInMemoryDatabase(inMemoryDbName)); ;
			}
			else
			{
				services.AddDbContext<DbContext, ECommerceContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("ECommerce")
				));
			}

			services.AddScoped<IUnitOfWork, UnitOfWork>();

			//services.AddHttpContextAccessor();

			// I choose not to register the repositories manually, because they should really only be used in conjunction with IUnitOfWork

			return services;
		}
	}
}
