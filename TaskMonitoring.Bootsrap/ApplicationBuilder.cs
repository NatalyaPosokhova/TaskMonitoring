using Microsoft.Extensions.DependencyInjection;
using System;
using TaskMonitoring.Cards.BL;
using TaskMonitoring.Cards.BL.Interface;
using TaskMonitoring.Cards.DataAccess;
using TaskMonitoring.Cards.DataAccess.Interface;
using TaskMonitoring.Utilities;
using TaskMonitoring.APIClients.Users.Interfaces;

namespace TaskMonitoring.Bootsrap
{
	public static class ApplicationBuilder
	{
		public static void Build(IServiceCollection services)
		{
			services.AddTransient<IDataAccess, DataAccess>();
			services.AddTransient<ITaskService, TaskService>();
			services.AddScoped<ContextFactory<TaskDbContext>>();
			services.AddScoped<IWebAPIUsers, WebAPIUsers>();
		}
	}
}
