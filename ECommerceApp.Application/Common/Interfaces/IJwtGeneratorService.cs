using ECommerceApp.Domain.Common.Models;
using ECommerceApp.Domain.Models;
using System.Threading.Tasks;

namespace ECommerceApp.Application.Common.Interfaces
{
	public interface IJwtGeneratorService
	{
		Task<Jwt> GenererateJwtFromLoginModelAsync(UserCommandDto loginModel);
	}
}
