using Microsoft.Extensions.DependencyInjection;
using TaskMonitoring.Users.BL;
using TaskMonitoring.Users.BL.Interface;
using TaskMonitoring.Users.DataAccess;
using TaskMonitoring.Users.DataAccess.Interface;
using TaskMonitoring.Utilities;

namespace TaskMonitoring.Users.Bootstrap
{
	public static class ApplicationBuilder
	{
		public static void Build(IServiceCollection services)
		{
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<ContextFactory<UserDbContext>>();
			services.AddScoped<IDataAccess, DataAccess.DataAccess>();
		}
	}
}
