using System.Linq;

namespace TaskMonitoring.Users.DataAccess.Interface.Models
{
	public class User
	{
		public long Id { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public IQueryable<long> TasksIds { get; set; }
	}
}
