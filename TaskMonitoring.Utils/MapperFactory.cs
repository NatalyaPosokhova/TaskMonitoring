using AutoMapper;
using System;

namespace TaskMonitoring.Utilities
{
	internal static class MapperFactory<S,D> 
	{
		internal static Mapper CreateMapper(Action<IMappingExpression<S,D>> userSetup = null)
		{
			var config = new MapperConfiguration(cfg => { var expression = cfg.CreateMap<S, D>(); if(userSetup != null) userSetup(expression); });
			
			return new Mapper(config);
		}
	}
}
