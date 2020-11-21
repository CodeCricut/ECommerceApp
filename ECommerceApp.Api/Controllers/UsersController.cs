using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.UserCQs.Commands.ModifyUser;
using ECommerceApp.Application.UserCQs.Commands.RegisterUser;
using ECommerceApp.Application.UserCQs.Commands.RemoveUser;
using ECommerceApp.Application.UserCQs.Queries.GetAuthenticatedUser;
using ECommerceApp.Application.UserCQs.Queries.GetById;
using ECommerceApp.Application.UserCQs.Queries.GetByIds;
using ECommerceApp.Application.UserCQs.Queries.GetBySearch;
using ECommerceApp.Application.UserCQs.Queries.GetByUsername;
using ECommerceApp.Application.UserCQs.Queries.GetByUsernames;
using ECommerceApp.Domain.Common.Models;
using ECommerceApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceApp.Api.Controllers
{
	[Route("api/[controller]")]
	public class UsersController : ApiController
	{
		private readonly IJwtGeneratorService _jwtGeneratorService;

		public UsersController(IJwtGeneratorService jwtGeneratorService)
		{
			_jwtGeneratorService = jwtGeneratorService;
		}

		#region Commands
		[HttpPut]
		public async Task<ActionResult<UserQueryDto>> PutAsync([FromBody] UserCommandDto updateModel)
		{
			return Ok(await Mediator.Send(new ModifyUserCommand(updateModel)));
		}

		[HttpPost("register")]
		public async Task<ActionResult<UserQueryDto>> PostAsync([FromBody] UserCommandDto registerModel)
		{
			return Ok(await Mediator.Send(new RegisterUserCommand(registerModel)));
		}

		[HttpDelete]
		public async Task<ActionResult<UserQueryDto>> DeleteAsync()
		{
			return Ok(await Mediator.Send(new RemoveCurrentUserCommand()));
		}
		#endregion

		#region Queries
		[HttpGet("me")]
		public async Task<ActionResult<UserQueryDto>> Me()
		{
			return Ok(await Mediator.Send(new GetAuthenticatedUserQuery()));
		}

		[HttpGet("{key:int}")]
		public async Task<ActionResult<UserQueryDto>> GetById(int key)
		{
			// probably a code smell but yolo.
			return Ok(await Mediator.Send(new GetUserByIdQuery(key)));
		}

		[HttpGet("range")]
		public async Task<ActionResult<PaginatedList<UserQueryDto>>> GetByIdsAsync([FromQuery] IEnumerable<int> id, [FromQuery] PagingParams pagingParams)
		{
			return await Mediator.Send(new GetUsersByIdsQuery(id, pagingParams));
		}

		[HttpGet("search")]
		public async Task<ActionResult<PaginatedList<UserQueryDto>>> SearchAsync([FromQuery] string searchTerm, [FromQuery] PagingParams pagingParams)
		{
			return await Mediator.Send(new GetUsersBySearchQuery(searchTerm, pagingParams));
		}

		[HttpGet("user/{username:string}")]
		public async Task<ActionResult<UserQueryDto>> GetUserAsync(string username)
		{
			return await Mediator.Send(new GetUserByUsernameQuery(username));
		}

		[HttpGet("users")]
		public async Task<ActionResult<PaginatedList<UserQueryDto>>> GetUsersAsync([FromQuery] IEnumerable<string> usernames, [FromQuery] PagingParams pagingParams)
		{
			return await Mediator.Send(new GetUsersByUsernamesQuery(usernames, pagingParams));
		}


		[HttpGet("jwt")]
		public async Task<ActionResult<Jwt>> GetJwt(UserCommandDto loginModel)
		{
			return await _jwtGeneratorService.GenererateJwtFromLoginModelAsync(loginModel);
		}
		#endregion
	}
}
