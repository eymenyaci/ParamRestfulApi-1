﻿using WebApi.Mapper;

namespace WebApi.Extensions
{
    public static class MapToExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return AutoMapperConfig.Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return AutoMapperConfig.Mapper.Map(source, destination);
        }
    }
}
