using AutoMapper;

namespace TaskMonitoring.Utilities
{
	public static class MapperFactory<S,D> 
	{
		public static Mapper CreateMapper()
		{
			var config = new MapperConfiguration(cfg => cfg.CreateMap<S, D>());
			return new Mapper(config);
		}
	}
}
