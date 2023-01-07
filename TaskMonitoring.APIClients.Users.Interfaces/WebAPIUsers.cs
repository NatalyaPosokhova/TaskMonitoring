using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMonitoring.APIClients.Users.Interfaces.DTO;

namespace TaskMonitoring.APIClients.Users.Interfaces
{
	public class WebAPIUsers : WebProxy, IWebAPIUsers
	{
		private readonly string Url = "http://localhost:5185/User/";
		public async Task<User> CreateUser(string login, string password)
		{
			try
			{
				string query = Url + "CreateUser";
				return await PostAsync<User>(query, new { login = login, password = password });
			}
			catch(HttpRequestException e)
			{
				throw new WebProxyException(e);
			}
		}

		public async Task DeleteUser(long id)
		{
			try
			{
				string query = Url + "DeleteUser";
				await PostAsync<User>(query, new { id = id });
			}
			catch(HttpRequestException e)
			{
				throw new WebProxyException(e);
			}
		}

		public async Task<User> GetUserById(long id)
		{
			try
			{
				string query = Url + "GetUserById";
				return await GetAsync<User>(query, new { userId = id });
			}
			catch(HttpRequestException e)
			{
				throw new WebProxyException(e);
			}
		}
	}
}
