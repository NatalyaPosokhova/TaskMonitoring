using System.Collections.Generic;
using TaskMonitoring.Users.BL.Interface;
using TaskMonitoring.Users.BL.Interface.DTO;

namespace TaskMonitoring.Users.BL
{
	public class UserService : IUserService
	{
		public UserDTO CreateUser(string login, string password)
		{
			throw new System.NotImplementedException();
		}

		public void DeleteUser(long id)
		{
			throw new System.NotImplementedException();
		}

		public UserDTO GetUserById(long id)
		{
			throw new System.NotImplementedException();
		}

		public void UpdateUserPassword(long id, string newPassword)
		{
			throw new System.NotImplementedException();
		}
	}
}
