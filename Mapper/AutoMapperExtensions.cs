using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventDemoAPI.Mapper
{
    public static class AutoMapperExtensions
    {
        public static List<TDestination> MapList<TSource, TDestination>(IMapper mapper, List<TSource> source)
        {
            return mapper.Map<List<TDestination>>(source);
        }
    }
}
