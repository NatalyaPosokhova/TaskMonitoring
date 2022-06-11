using Microsoft.EntityFrameworkCore;
using TaskMonitoring.Cards.DataAccess.Models;

namespace TaskMonitoring.Cards.DataAccess
{
	public class TaskDbContext : DbContext
	{
		public DbSet<User> Users => Set<User>();
		public DbSet<Task> Tasks => Set<Task>();
		public DbSet<Comment> Comments => Set<Comment>();
		public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
		{
		}

		public TaskDbContext()
		{
		}

		//public TaskDbContext() => Database.EnsureCreated();

	}
}
