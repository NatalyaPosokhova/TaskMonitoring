using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMonitoring.APIClients
{
	public class WebProxy
	{
		public async Task<TResponse> GetAsync<TRequest, TResponse>(string query, TRequest request)
		{
			throw new NotImplementedException();
		}

		public async Task<TResponse> PostAsync<TRequest, TResponse>(string query, TRequest responce)
		{
			throw new NotImplementedException();
		}

	}
}
