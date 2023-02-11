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
		public TaskDTO CreateTask(long userId, TaskDTO task);
		public void DeleteTaskById(long userId, long taskId);
		public void AddComment(long userId, long taskId, string comment);
		public IEnumerable<TaskDTO> GetAllTasks(long userId);
		public void UpdateTask(long userId, TaskDTO task);
		public TaskDTO GetTaskById(long userId, long taskId);
	}
}
