using ECommerceApp.Domain.Common;

namespace ECommerceApp.Domain.Interfaces
{
	public interface IEntityRepository<TEntity> where TEntity : DomainEntity
	{
	}
}
