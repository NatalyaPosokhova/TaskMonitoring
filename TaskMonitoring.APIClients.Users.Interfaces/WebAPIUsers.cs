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
		public async Task<User> GetUserById(long id)
		{
			try
			{
				string query = $"http://localhost:5185/User/GetUserById";
				return await GetAsync<User>(query, new { userId = id });
			}
			catch(HttpRequestException e)
			{
				throw new WebProxyException(e);
			}
		}
	}
}
