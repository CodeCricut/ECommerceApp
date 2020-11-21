using Microsoft.Extensions.DependencyInjection;

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
