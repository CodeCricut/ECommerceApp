using ECommerceApp.Api.Configuration;
using ECommerceApp.Api.Services;
using ECommerceApp.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace ECommerceApp.Api
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddCors(opt =>
			{
				opt.AddPolicy(name: "DefaultCorsPolicy",
					builder => builder.AllowAnyOrigin());
			});

			// Register the Swagger generator, defining 1 or more Swagger documents
			services.AddSwaggerGen(opt =>
			{
				opt.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "ECommerceApp API",
					Description = "A basic e-commerce api.",
					Contact = new OpenApiContact
					{
						Name = "A. Joseph Richerson",
						Email = string.Empty
					}
				});

				// Set the comments path for the Swagger JSON and UI.**
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				opt.IncludeXmlComments(xmlPath);

				// Add JWT support to the UI
				opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "JWT Authorization header using the Bearer scheme."
				});

				opt.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						  new OpenApiSecurityScheme
							{
								Reference = new OpenApiReference
								{
									Type = ReferenceType.SecurityScheme,
									Id = "Bearer"
								}
							},
							new string[] {}

					}
				});
			});

			services.AddControllers();
			services.AddMvcCore();

			services.Configure<JwtSettings>(options => configuration.GetSection("JwtSettings").Bind(options));
			services.AddScoped<IJwtGeneratorService, JwtGeneratorService>();
			services.AddSingleton<ICurrentUserService, CurrentUserService>();

			return services;
		}
	}
}
