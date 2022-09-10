using System.Collections.Generic;
using TaskMonitoring.Users.DataAccess.Interface.Models;

namespace TaskMonitoring.Users.DataAccess.Interface
{
	public interface IDataAccess
	{
		public long CreateUser(User user);
		public User GetUserById(long id);
		public void UpdateUser(User user);
		public void DeleteUser(long id);
		public IEnumerable<long> GetAllUserTasks(long id);
	}
}
