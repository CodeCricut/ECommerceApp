using ECommerceApp.Api;
using ECommerceApp.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Application.IntegrationTests.Common
{
	public class AppIntegrationTest : IDisposable
	{
		public AppIntegrationTest()
		{
			Factory = new CustomWebApplicationFactory<ECommerceApp.Api.Startup>();
			Client = Factory.CreateClient();
		}

		public CustomWebApplicationFactory<Startup> Factory { get; private set; }
		public HttpClient Client { get; private set; }

		public void Dispose()
		{
			using var scope = Factory.Services.CreateScope();
			var context = (ECommerceContext)scope.ServiceProvider.GetService<DbContext>();
			context.ClearDatabase();
			context.InitializeForTests();
		}
	}
}
