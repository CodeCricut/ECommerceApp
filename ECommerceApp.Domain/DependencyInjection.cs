using AutoMapper;
using ECommerceApp.Domain.Common.Mapping;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ECommerceApp.Domain
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddDomain(this IServiceCollection services)
		{
			return services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));
		}
	}
}
