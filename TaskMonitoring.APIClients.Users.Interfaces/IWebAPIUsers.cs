
using TaskMonitoring.APIClients.Users.Interfaces.DTO;

namespace TaskMonitoring.APIClients.Users.Interfaces
{
	public interface IWebAPIUsers
	{
		public User GetUserById(long Id);
	}
}
