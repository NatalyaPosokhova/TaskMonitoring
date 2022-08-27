using AutoMapper;
using System;

namespace TaskMonitoring.Utilities
{
	public static class MapperFactory<S,D> 
	{
		internal static Mapper CreateMapper(Action<IMapperConfigurationExpression> userSetup = null)
		{
			var config = new MapperConfiguration(cfg =>
			{
				if(userSetup != null)
					userSetup(cfg);
				cfg.CreateMap<S, D>();
			});

			return new Mapper(config);
		}
	}
}
