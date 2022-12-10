using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMonitoring.APIClients
{
	public class WebProxy
	{
		private readonly HttpClient client;
		public WebProxy()
		{
			client = new HttpClient();
		}
		public async Task<TResponse?> GetAsync<TRequest, TResponse>(string query, TRequest request)
		{
			try
			{
				var response = await client.GetAsync(query);
				var content = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<TResponse>(content);
			}
			catch(Exception ex)
			{
				throw new WebProxyException(ex);
			}
		}

		public async Task<TResponse?> PostAsync<TRequest, TResponse>(string query, TRequest request)
		{
			try
			{
				var json = JsonConvert.SerializeObject(request);
				var data = new StringContent(json, Encoding.UTF8, "application/json");

				var response = await client.PostAsync(query, data);
				var content = await response.Content.ReadAsStringAsync();

				return JsonConvert.DeserializeObject<TResponse>(content);
			}
			catch(Exception ex)
			{
				throw new WebProxyException(ex);
			}
		}

	}
}
