﻿using AutoMapper;
using ECommerceApp.Domain.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApp.Domain.Common.Mapping
{
	public static class MappingExtensions
	{
		public static Task<PaginatedList<TDestination>> ToPaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, PagingParams pagingParams)
			=> PaginatedList<TDestination>.CreateAsync(queryable, pagingParams.PageNumber, pagingParams.PageSize);

		public static Task<PaginatedList<TDestination>> ToMappedPaginatedListAsync<TSource, TDestination>(this PaginatedList<TSource> list, IMapper mapper)
		{
			return Task.Factory.StartNew(() =>
			{
				IEnumerable<TDestination> sourceList = mapper.Map<IEnumerable<TDestination>>(list.Items);
				PaginatedList<TDestination> pagedResult = new PaginatedList<TDestination>(sourceList.ToList(), list.TotalCount, list.PageIndex, list.PageSize);
				return pagedResult;
			});
		}
	}
}
