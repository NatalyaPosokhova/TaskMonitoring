using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMonitoring.Cards.BL.Interface.DTO;

namespace TaskMonitoring.Cards.BL.Interface
{
	public interface ITaskService
	{
		public Task<TaskDTO> CreateTask(long userId, TaskDTO task);
		public Task DeleteTaskById(long userId, long taskId);
		public Task AddComment(long userId, long taskId, string comment);
		public Task<IEnumerable<TaskDTO>> GetAllTasks(long userId);
		public Task UpdateTask(long userId, TaskDTO task);
	}
}
