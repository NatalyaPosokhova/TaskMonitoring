using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskMonitoring.Cards.BL.Interface.DTO;
using TaskMonitoring.Cards.BL.Interface;

namespace TaskMonitoring.Cards.BL
{
	public class TaskService : ITaskService
	{
		public void AddComment(long taskId, string comment)
		{
			throw new NotImplementedException();
		}

		public Task CreateTask(long userId)
		{
			throw new NotImplementedException();
		}

		public void DeleteTaskById(long taskId)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Task> GetAllTasks(long userId)
		{
			throw new NotImplementedException();
		}

		public void SaveTask(Task task)
		{
			throw new NotImplementedException();
		}
	}
}
