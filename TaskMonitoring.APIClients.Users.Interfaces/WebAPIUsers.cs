using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMonitoring.APIClients.Users.Interfaces.DTO;

namespace TaskMonitoring.APIClients.Users.Interfaces
{
	public class WebAPIUsers : IWebAPIUsers
	{
		public User GetUserById(long Id)
		{
			throw new NotImplementedException();
		}
	}
}
