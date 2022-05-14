using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskMonitoring.Cards.DataAccess.Interface
{
	public interface IDataAccess
	{
		public long AddTask(long userId, TaskDataAccessDTO task);
		public void DeleteTask(long taskId);
		public void UpdateTask(TaskDataAccessDTO task);
		public void AddComment(long taskId, string comment);
		public IEnumerable<TaskDataAccessDTO> GetAllTasksByUserId(long userId);
		public TaskDataAccessDTO GetTaskById(long taskId);
	}
}
