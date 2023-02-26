using TaskMonitoring.APIClients.Tasks.DTO;

namespace TaskMonitoring.APIClients.Tasks
{
	public interface IWebAPITasks
	{
		public Task<TaskProxyDTO> CreateTask(long userId, TaskProxyDTO task);
		public Task DeleteTaskById(long userId, long taskId);
		public Task AddComment(long userId, long taskId, string comment);
		public Task<IEnumerable<TaskProxyDTO>> GetAllTasks(long userId);
		public Task UpdateTask(long userId, TaskProxyDTO task);
		public Task<TaskProxyDTO> GetTaskById(long userId, long taskId);
	}
}