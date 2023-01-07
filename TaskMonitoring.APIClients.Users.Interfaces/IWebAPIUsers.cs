
using TaskMonitoring.APIClients.Users.Interfaces.DTO;

namespace TaskMonitoring.APIClients.Users.Interfaces
{
	public interface IWebAPIUsers
	{
		public Task<User> GetUserById(long Id);
		public Task<User> CreateUser(string login, string password);
		public Task DeleteUser(long id);
	}
}
