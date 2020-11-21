using ECommerceApp.Application.ProductListingCQs.Commands.AddProductListing;
using ECommerceApp.Application.ProductListingCQs.Commands.ModifyProductListing;
using ECommerceApp.Application.ProductListingCQs.Commands.RemoveProductListing;
using ECommerceApp.Application.ProductListingCQs.Queries.GetById.GetProductListingById;
using ECommerceApp.Application.ProductListingCQs.Queries.GetByIds;
using ECommerceApp.Application.ProductListingCQs.Queries.GetBySearch;
using ECommerceApp.Domain.Common.Models;
using ECommerceApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceApp.Api.Controllers
{
	[Route("api/listings")]
	public class ProductListingsController : ApiController
	{
		#region Commands
		[HttpPost]
		public async Task<ActionResult<ProductListingQueryDto>> ListProductAsync([FromBody] ProductListingCommandDto listCommand)
		{
			return Ok(await Mediator.Send(new AddProductListingCommand(listCommand)));
		}

		[HttpPut]
		public async Task<ActionResult<ProductListingQueryDto>> ModifyProductListingAsync([FromBody] ProductListingCommandDto modifyCommand)
		{
			return Ok(await Mediator.Send(new ModifyProductListingCommand(modifyCommand)));
		}

		[HttpDelete("{key:int}")]
		public async Task<ActionResult<ProductListingQueryDto>> RemoveProductListingAsync([FromQuery] int key)
		{
			return Ok(await Mediator.Send(new RemoveProductListingCommand(key)));
		}
		#endregion

		#region Queries
		[HttpGet("{key:int}")]
		public async Task<ActionResult<ProductListingQueryDto>> GetAsync([FromQuery] int key)
		{
			return Ok(await Mediator.Send(new GetProductListingByIdQuery(key)));
		}

		[HttpGet("range")]
		public async Task<ActionResult<PaginatedList<ProductListingQueryDto>>> GetByIdsAsync([FromQuery] IEnumerable<int> id, PagingParams pagingParams)
		{
			return Ok(await Mediator.Send(new GetProductListingsByIdsQuery(id, pagingParams)));
		}

		[HttpGet("search")]
		public async Task<ActionResult<PaginatedList<ProductListingQueryDto>>> SearchAsync([FromQuery] string searchTerm, PagingParams pagingParams)
		{
			return Ok(await Mediator.Send(new GetProductListingsBySearchQuery(searchTerm, pagingParams)));
		}
		#endregion
	}
}
