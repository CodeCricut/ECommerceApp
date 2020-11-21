using ECommerceApp.Api.Configuration;
using ECommerceApp.Api.Services;
using ECommerceApp.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceApp.Api
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddControllers();
			services.Configure<JwtSettings>(options => configuration.GetSection("JwtSettings").Bind(options));
			services.AddScoped<IJwtGeneratorService, JwtGeneratorService>();
			services.AddSingleton<ICurrentUserService, CurrentUserService>();

			return services;
		}
	}
}
