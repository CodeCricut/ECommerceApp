using ECommerceApp.Api.Configuration;
using ECommerceApp.Application.Common.Interfaces;
using ECommerceApp.Application.UserCQs.Queries.GetUserFromLoginModel;
using ECommerceApp.Domain.Common.Models;
using ECommerceApp.Domain.Exceptions;
using ECommerceApp.Domain.Models;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Api.Services
{
	class JwtGeneratorService : IJwtGeneratorService
	{
		private readonly IMediator _mediator;
		private JwtSettings _jwtSettings;

		public JwtGeneratorService(IOptions<JwtSettings> options, IMediator mediator)
		{
			_jwtSettings = options.Value;
			_mediator = mediator;
		}


		public async Task<Jwt> GenererateJwtFromLoginModelAsync(UserCommandDto loginModel)
		{
			var user = await _mediator.Send(new GetUserFromLoginModelQuery(loginModel));
			if (user.Errors.Count > 0) throw new NotFoundException();

			// generate token that is valid for 7 days
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
			string tokenString = tokenHandler.WriteToken(securityToken);
			Jwt token = new Jwt(securityToken.ValidTo, tokenString);
			return token;
		}
	}
}
