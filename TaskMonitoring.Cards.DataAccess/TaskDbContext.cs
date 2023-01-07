using Microsoft.EntityFrameworkCore;
using TaskMonitoring.Cards.DataAccess.Models;

namespace TaskMonitoring.Cards.DataAccess
{
	public class TaskDbContext : DbContext
	{
		public DbSet<Task> Tasks => Set<Task>();
		public DbSet<Comment> Comments => Set<Comment>();

#if _FOR_MIGRATE_
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseNpgsql();
		}

		public TaskDbContext()
		{

			Database.SetConnectionString("Server=localhost;Port=5432;Database=taskdb;User ID=postgres;Password=qwerty");
			Database.EnsureCreated();
		}
#else
		public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
		{
			Database.EnsureCreated();
		}
#endif
	}
}
