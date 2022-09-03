using AutoMapper;
using System;

namespace TaskMonitoring.Utilities
{
	public static class MapperFactory<S,D> 
	{
		internal static Mapper CreateMapper(Action<IMappingExpression<S,D>> userSetup = null)
		{
			var config = new MapperConfiguration(cfg =>
			{
				var mappingExpr = cfg.CreateMap<S, D>();
				if(userSetup != null)
					userSetup(mappingExpr);
			});

			return new Mapper(config);
		}
	}
}
