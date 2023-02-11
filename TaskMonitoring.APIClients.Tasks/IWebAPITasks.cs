using TaskMonitoring.APIClients.Tasks.DTO;
using TaskMonitoring.Cards.Data;

namespace TaskMonitoring.APIClients.Tasks
{
	public interface IWebAPITasks
	{
		public Task<TaskProxyDTO> CreateTask(long userId, NewTaskCard task);
		public Task DeleteTaskById(long userId, long taskId);
		public Task AddComment(long userId, long taskId, string comment);
		public Task<IEnumerable<TaskProxyDTO>> GetAllTasks(long userId);
		public Task UpdateTask(long userId, NewTaskCard task);
		public Task<TaskProxyDTO> GetTaskById(long userId, long taskId);
	}
}