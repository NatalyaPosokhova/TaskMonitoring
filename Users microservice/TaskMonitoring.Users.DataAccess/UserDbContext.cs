using Microsoft.EntityFrameworkCore;
using TaskMonitoring.Users.DataAccess.Interface.Models;

namespace TaskMonitoring.Users.DataAccess
{
	public class UserDbContext : DbContext
	{
		public DbSet<User> Users => Set<User>();
		public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
		{
			Database.EnsureCreated();
		}
	}
}
