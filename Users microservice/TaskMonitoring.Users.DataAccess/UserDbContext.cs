using Microsoft.EntityFrameworkCore;
using TaskMonitoring.Users.DataAccess.Interface.Models;

namespace TaskMonitoring.Users.DataAccess
{
	public class UserDbContext : DbContext
	{
		public DbSet<User> Users => Set<User>();
		public UserDbContext()
		{

		}
		public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
		{
			Database.EnsureCreated();
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if(!optionsBuilder.IsConfigured)
				optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=userdb;Username=postgres;Password=qwerty");
		}
	}
}
