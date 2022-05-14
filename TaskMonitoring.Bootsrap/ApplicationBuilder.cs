using Microsoft.Extensions.DependencyInjection;
using System;
using TaskMonitoring.Cards.DataAccess;
using TaskMonitoring.Cards.DataAccess.Interface;

namespace TaskMonitoring.Bootsrap
{
	public static class ApplicationBuilder
	{
		public static void Build(IServiceCollection services)
		{
			services.AddTransient<IDataAccess, DataAccess>();
		}
	}
}
