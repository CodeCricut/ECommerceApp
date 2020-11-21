using AutoMapper;

namespace ECommerceApp.Domain.Common.Mapping
{
	public interface IMapFrom<T>
	{
		void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
	}
}
