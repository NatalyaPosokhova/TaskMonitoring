using System;
using System.Collections.Generic;
using TaskMonitoring.Cards.BL.Interface.DTO;
using TaskMonitoring.Cards.DataAccess.Interface;

namespace TaskMonitoring.Cards.DataAccess
{
	public class DataAccess : IDataAccess
	{
		public void AddComment(long taskId, string comment)
		{
			throw new NotImplementedException();
		}

		public long AddTask(long userId, Task task)
		{
			throw new NotImplementedException();
		}

		public void DeleteTask(long taskId)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Task> GetAllTasksByUserId(long userId)
		{
			throw new NotImplementedException();
		}

		public void UpdateTask(Task task)
		{
			throw new NotImplementedException();
		}
	}
}
