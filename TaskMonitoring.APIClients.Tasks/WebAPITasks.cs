using TaskMonitoring.APIClients.Tasks.DTO;

namespace TaskMonitoring.APIClients.Tasks
{
	public class WebAPITasks : IWebAPITasks
	{
		private readonly string Url = "https://localhost:44380/Task/";
		private readonly WebProxy _proxy;
		public WebAPITasks()
		{
			_proxy = new WebProxy();
		}
		public async Task AddComment(long userId, long taskId, string comment)
		{
			try
			{
				string query = Url + "AddComment";
				await _proxy.PostAsync<object>(query, new { userId, taskId, comment });
			}
			catch(HttpRequestException e)
			{
				throw new WebProxyException(e);
			}
		}

		public async Task<TaskProxyDTO> CreateTask(long userId, TaskProxyDTO task)
		{
			try
			{
				string query = Url + "CreateUser";
				return await _proxy.PostAsync<TaskProxyDTO>(query, new { userId, task });
			}
			catch(HttpRequestException e)
			{
				throw new WebProxyException(e);
			}
		}

		public async Task DeleteTaskById(long userId, long taskId)
		{
			try
			{
				string query = Url + "DeleteTaskById";
				await _proxy.PostAsync<TaskProxyDTO>(query, new { userId, taskId });
			}
			catch(HttpRequestException e)
			{
				throw new WebProxyException(e);
			}
		}

		public async Task<IEnumerable<TaskProxyDTO>> GetAllTasks(long userId)
		{
			try
			{
				string query = Url + "GetAllTasks";
				return await _proxy.GetAsync<IEnumerable<TaskProxyDTO>>(query, new { userId});
			}
			catch(HttpRequestException e)
			{
				throw new WebProxyException(e);
			}
		}

		public async Task<TaskProxyDTO> GetTaskById(long userId, long taskId)
		{
			try
			{
				string query = Url + "GetTaskById";
				return await _proxy.GetAsync<TaskProxyDTO>(query, new { userId, taskId });
			}
			catch(HttpRequestException e)
			{
				throw new WebProxyException(e);
			}
		}

		public async Task UpdateTask(long userId, TaskProxyDTO task)
		{
			try
			{
				string query = Url + "UpdateTask";
				await _proxy.PostAsync<object>(query, new { userId, task });
			}
			catch(HttpRequestException e)
			{
				throw new WebProxyException(e);
			}
		}
	}
}
