using ECommerceApp.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UnitTests
{
	public static class ServiceProviderFactory
	{
		private static IServiceProvider _serviceProvider;
		public static IServiceProvider CreateServiceProvider()
		{
			if (_serviceProvider != null) return _serviceProvider;

			var services = new ServiceCollection();

			services.AddDomain();

			_serviceProvider = services.BuildServiceProvider();
			return _serviceProvider; 
		}
	}
}
