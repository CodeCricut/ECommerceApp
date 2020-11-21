using ECommerceApp.Api.Pipeline;
using ECommerceApp.Application.ProductDetailsCQs.Commands.BuyProduct;
using ECommerceApp.Application.ProductDetailsCQs.Commands.ModifyProductDetails;
using ECommerceApp.Application.ProductDetailsCQs.Commands.ReturnProduct;
using ECommerceApp.Application.ProductDetailsCQs.Queries.GetById;
using ECommerceApp.Application.ProductDetailsCQs.Queries.GetByIds;
using ECommerceApp.Domain.Common.Models;
using ECommerceApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceApp.Api.Controllers
{
	[Route("api/details")]
	public class ProductDetailsController : ApiController
	{
		#region Commands
		[HttpPost("buy")]
		[JwtAuthorize]
		public async Task<ActionResult<ProductDetailsQueryDto>> BuyProductAsync([FromBody] ProductDetailsCommandDto buyCommand)
		{
			return Ok(await Mediator.Send(new BuyProductCommand(buyCommand)));
		}

		// Depending on what the modify command does, this endpoint may be unneccesary.
		[HttpPost("modify")]
		[JwtAuthorize]
		public async Task<ActionResult<ProductDetailsQueryDto>> ModifyProductAsync([FromBody] ProductDetailsCommandDto modifyCommand)
		{
			return Ok(await Mediator.Send(new ModifyProductDetailsCommand(modifyCommand)));
		}

		[HttpPost("return")]
		[JwtAuthorize]
		public async Task<ActionResult<ProductDetailsQueryDto>> ReturnProductAsync([FromBody] ProductDetailsCommandDto returnCommand)
		{
			return Ok(await Mediator.Send(new ReturnProductCommand(returnCommand)));
		}
		#endregion

		#region Queries
		[HttpGet("{key:int}")]
		public async Task<ActionResult<ProductDetailsQueryDto>> GetAsync([FromQuery] int key)
		{
			return Ok(await Mediator.Send(new GetProductDetailsByIdQuery(key)));
		}

		[HttpGet("range")]
		public async Task<ActionResult<PaginatedList<ProductDetailsQueryDto>>> GetAsync([FromQuery] IEnumerable<int> id, PagingParams pagingParams)
		{
			return Ok(await Mediator.Send(new GetProductDetailsByIdsQuery(id, pagingParams)));
		}
		#endregion
	}
}
