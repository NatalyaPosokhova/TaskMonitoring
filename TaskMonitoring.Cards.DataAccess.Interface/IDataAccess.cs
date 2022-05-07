using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskMonitoring.Cards.BL.Interface.DTO;

namespace TaskMonitoring.Cards.DataAccess.Interface
{
	public interface IDataAccess
	{
		public long AddTask(long userId, Task task);
		public void DeleteTask(long taskId);
		public void UpdateTask(Task task);
		public void AddComment(long taskId, string comment);
		public IEnumerable<Task> GetAllTasksByUserId(long userId);
		public Task GetTaskById(long taskId);
	}
}
