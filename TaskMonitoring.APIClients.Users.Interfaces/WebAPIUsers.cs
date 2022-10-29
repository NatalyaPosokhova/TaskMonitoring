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
		public async Task<User> GetUserById(long Id)
		{
			try
			{
				string query = $"/api/User/GetUserById/";
				return await GetAsync<long, User>(query, Id);
			}
			catch(HttpRequestException e)
			{
				throw new WebProxyException(e);
			}
		}
	}
}
