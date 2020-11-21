namespace ECommerceApp.Domain.Common
{
	public abstract class BaseUser : DomainEntity
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
}
