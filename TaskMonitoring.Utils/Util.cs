using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMonitoring.Utilities
{
	public static class Util<S,D>
	{
		public static D Map(S source, Action<IMappingExpression<S, D>> userSetup = null)
		{
			var mapper = MapperFactory<S, D>.CreateMapper(userSetup);
			return mapper.Map<D>(source);
		}
	}
}
