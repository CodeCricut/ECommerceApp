using ECommerceApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.Domain.Interfaces
{
	public interface IEntityRepository<TEntity> where TEntity : DomainEntity
	{
	}
}
