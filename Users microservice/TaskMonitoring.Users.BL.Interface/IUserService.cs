using System.Collections.Generic;
using TaskMonitoring.Users.BL.Interface.DTO;

namespace TaskMonitoring.Users.BL.Interface
{
	public interface IUserService
	{
		public UserDTO CreateUser(string login, string password);
		public UserDTO GetUserById(long id);
		public void UpdateUserPassword(long id, string newPassword);
		public void DeleteUser(long id);
	}
}
