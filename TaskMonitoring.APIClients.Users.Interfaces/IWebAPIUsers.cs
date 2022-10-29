
using TaskMonitoring.APIClients.Users.Interfaces.DTO;

namespace TaskMonitoring.APIClients.Users.Interfaces
{
	public interface IWebAPIUsers
	{
		public Task<User> GetUserById(long Id);
	}
}
