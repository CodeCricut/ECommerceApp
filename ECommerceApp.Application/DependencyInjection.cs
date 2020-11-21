﻿using ECommerceApp.Application.Common.PipelineBehaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ECommerceApp.Application
{
	public static class DependencyInjection 
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			var assembly = Assembly.GetExecutingAssembly();

			services.AddMediatR(assembly);

			// Adds validators to DI
			services.AddValidatorsFromAssembly(assembly);

			// Add validation behavior.
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

			return services;
		}
	}
}
