using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMonitoring.APIClients
{
	public class WebProxy<TRequest, TResponse>
	{
		public async Task<TResponse> GetAsync(string query, TRequest request)
		{
			throw new NotImplementedException();
		}

		public async Task<TResponse> PostAsync(string query, TRequest responce)
		{
			throw new NotImplementedException();
		}

	}
}
