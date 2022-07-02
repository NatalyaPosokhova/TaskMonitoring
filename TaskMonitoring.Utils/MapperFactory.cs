using AutoMapper;

namespace TaskMonitoring.Utilities
{
	internal static class MapperFactory<S,D> 
	{
		internal static Mapper CreateMapper()
		{
			var config = new MapperConfiguration(cfg => cfg.CreateMap<S, D>());
			return new Mapper(config);
		}
	}
}
