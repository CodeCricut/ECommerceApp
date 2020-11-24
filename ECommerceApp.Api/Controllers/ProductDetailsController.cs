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
		[HttpPost("modify/{key:int}")]
		[JwtAuthorize]
		public async Task<ActionResult<ProductDetailsQueryDto>> ModifyProductAsync([FromRoute] int key, [FromBody] ProductDetailsCommandDto modifyCommand)
		{
			return Ok(await Mediator.Send(new ModifyProductDetailsCommand(key, modifyCommand)));
		}

		[HttpPost("return/{key:int}")]
		[JwtAuthorize]
		public async Task<ActionResult<ProductDetailsQueryDto>> ReturnProductAsync([FromRoute] int key, [FromBody] ProductDetailsCommandDto returnCommand)
		{
			return Ok(await Mediator.Send(new ReturnProductCommand(key, returnCommand)));
		}
		#endregion

		#region Queries
		[HttpGet("{key:int}")]
		public async Task<ActionResult<ProductDetailsQueryDto>> GetAsync([FromRoute] int key)
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
