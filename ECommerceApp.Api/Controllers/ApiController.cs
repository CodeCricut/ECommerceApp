using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceApp.Api.Controllers
{
	public class ApiController : ControllerBase
	{
		private IMediator _mediator;
		protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
	}
}
