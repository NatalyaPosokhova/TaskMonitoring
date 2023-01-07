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
		public async Task<TResponse> GetAsync<TResponse>(string url, object request)
		{
			try
			{
				var query = GetQueryFromObject(request);
				url += query;
				var response = await client.GetAsync(url);
				var content = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<TResponse>(content);
			}
			catch(Exception ex)
			{
				throw new WebProxyException(ex);
			}
		}

		public async Task<TResponse> PostAsync<TResponse>(string url, object request)
		{
			try
			{
				var json = JsonConvert.SerializeObject(request);
				var data = new StringContent(json, Encoding.UTF8, "application/json");

				var response = await client.PostAsync(url, data);
				var content = await response.Content.ReadAsStringAsync();

				return JsonConvert.DeserializeObject<TResponse>(content);
			}
			catch(Exception ex)
			{
				throw new WebProxyException(ex);
			}
		}

		private string GetQueryFromObject(object request)
		{
			if(request == null)
				return "";

			var properties = request.GetType().GetProperties()
			//var fields = request.GetType().GetFields()			
			.Select(p => $"{p.Name}={Convert.ToString(p.GetValue(request))}");

			return "?" + string.Join('&', properties);
		}
	}
}
