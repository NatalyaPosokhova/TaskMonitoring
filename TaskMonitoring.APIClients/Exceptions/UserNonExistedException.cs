using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMonitoring.APIClients.Exceptions
{
	public class UserNonExistedException : WebProxyException
	{
		public UserNonExistedException(Exception ex = null) : base(ex)
		{

		}
	}
}
