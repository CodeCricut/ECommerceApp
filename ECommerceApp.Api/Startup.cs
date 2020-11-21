using ECommerceApp.Api.Pipeline;
using ECommerceApp.Application;
using ECommerceApp.Domain;
using ECommerceApp.Persistance;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ECommerceApp.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDomain();
			services.AddPersistance(Configuration);
			services.AddApplication();
			services.AddApi();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DbContext dbContext)
		{
			if (env.IsDevelopment())
			{
				// Create the DB if it doesn't exist.
				dbContext.Database.EnsureCreated();

				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();
			app.UseCors("DefaultCorsPolicy");

			app.UseApiExceptionHandler();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
