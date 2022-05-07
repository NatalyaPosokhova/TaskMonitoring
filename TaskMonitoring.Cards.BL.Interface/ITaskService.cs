using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskMonitoring.Cards.BL.Interface.DTO;

namespace TaskMonitoring.Cards.BL.Interface
{
	public interface ITaskService
	{
		public Task CreateTask(long userId, Task task);
		public void DeleteTaskById(long taskId);
		public void AddComment(long taskId, string comment);
		public IEnumerable<Task> GetAllTasks(long userId);
	}
}
