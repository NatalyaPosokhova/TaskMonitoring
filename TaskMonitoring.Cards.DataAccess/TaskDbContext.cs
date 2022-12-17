using Microsoft.EntityFrameworkCore;
using TaskMonitoring.Cards.DataAccess.Models;

namespace TaskMonitoring.Cards.DataAccess
{
	public class TaskDbContext : DbContext
	{
		public DbSet<Task> Tasks => Set<Task>();
		public DbSet<Comment> Comments => Set<Comment>();
		public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
		{
			Database.EnsureCreated();
		}

		//public TaskDbContext() => Database.EnsureCreated();

	}
}
