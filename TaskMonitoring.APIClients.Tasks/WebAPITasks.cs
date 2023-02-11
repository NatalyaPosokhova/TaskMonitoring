using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMonitoring.APIClients.Tasks.DTO;
using TaskMonitoring.Cards.Data;

namespace TaskMonitoring.APIClients.Tasks
{
	public class WebAPITasks : IWebAPITasks
	{
		private readonly string Url = "https://localhost:44380/Task/";
		public Task AddComment(long userId, long taskId, string comment)
		{
			throw new NotImplementedException();
		}

		public Task<TaskProxyDTO> CreateTask(long userId, NewTaskCard task)
		{
			throw new NotImplementedException();
		}

		public Task DeleteTaskById(long userId, long taskId)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<TaskProxyDTO>> GetAllTasks(long userId)
		{
			throw new NotImplementedException();
		}

		public Task<TaskProxyDTO> GetTaskById(long userId, long taskId)
		{
			throw new NotImplementedException();
		}

		public Task UpdateTask(long userId, NewTaskCard task)
		{
			throw new NotImplementedException();
		}
	}
}
