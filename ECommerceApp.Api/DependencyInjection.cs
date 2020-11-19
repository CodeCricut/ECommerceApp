using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApp.Api
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApi(this IServiceCollection services)
		{
			services.AddControllers();

			return services;
		}
	}
}
