using System;
using System.Collections.Generic;
using TaskMonitoring.Cards.BL.Interface.DTO;
using TaskMonitoring.Cards.BL.Interface;
using TaskMonitoring.Cards.DataAccess.Interface;

namespace TaskMonitoring.Cards.BL
{
	public class TaskService : ITaskService
	{
		private IDataAccess _data;
		public TaskService()
		{
			_data = new TaskMonitoring.Cards.DataAccess.DataAccess();
		}

		public void AddComment(long taskId, string comment)
		{
			throw new NotImplementedException();
		}

		public Task CreateTask(long userId, Task task)
		{
			var taskId = _data.AddTask(userId, task);
			return _data.GetTaskById(taskId);
		}

		public void DeleteTaskById(long taskId)
		{
			_data.DeleteTask(taskId);
		}

		public IEnumerable<Task> GetAllTasks(long userId)
		{
			return _data.GetAllTasksByUserId(userId);
		}

	}
}
