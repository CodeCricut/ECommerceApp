﻿using ECommerceApp.Domain.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ECommerceApp.Api.Pipeline
{
	public static class ExceptionHandlerExceptions
	{
		public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder app)
		{
			return app.UseExceptionHandler(
				a => a.Run(async context =>
				{
					// TODO: learn about this little bit of magic.
					var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerFeature>();
					var exception = exceptionHandlerPathFeature.Error; // Your exception
					var code = 500; // Internal Server Error by default

					if (exception is NotFoundException) code = 404; // Not Found
					else if (exception is UnauthorizedException) code = 401; // Unauthorized
					else if (exception is InvalidPostException) code = 400; // Bad Request

					var result = JsonConvert.SerializeObject(new ErrorResponse(exception));

					context.Response.StatusCode = code;
					context.Response.ContentType = "application/json";
					await context.Response.WriteAsync(result);
				}));
		}
	}
}
