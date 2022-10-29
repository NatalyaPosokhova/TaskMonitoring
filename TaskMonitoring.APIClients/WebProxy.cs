using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMonitoring.APIClients
{
	public class WebProxy<TRequest, TResponse>
	{
		public async Task<TResponse> Get(string query, TRequest request)
		{
			throw new NotImplementedException();
		}
	}
}
