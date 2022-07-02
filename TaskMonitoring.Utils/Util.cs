using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMonitoring.Utilities
{
	public static class Util<S,D>
	{
		public static D MapFrom(S source)
		{
			var mapper = MapperFactory<S, D>.CreateMapper();
			return mapper.Map<D>(source);
		}
	}
}
