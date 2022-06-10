using Microsoft.EntityFrameworkCore;
using TaskMonitoring.Cards.DataAccess.Models;

namespace TaskMonitoring.Cards.DataAccess
{
	public class TaskDbContext : DbContext
	{
		public DbSet<Task> Tasks => Set<Task>();
		public DbSet<Comment> Comments => Set<Comment>();

		public TaskDbContext() => Database.EnsureCreated();

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
				optionsBuilder.UseNpgsql("Data Source=task.db");
		}
	}
}
