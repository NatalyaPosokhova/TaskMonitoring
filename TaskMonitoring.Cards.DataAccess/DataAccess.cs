using System;
using System.Collections.Generic;
using TaskMonitoring.Cards.DataAccess.Interface;
using TaskMonitoring.Cards.DataAccess.Models;

namespace TaskMonitoring.Cards.DataAccess
{
	public class DataAccess : IDataAccess, IDisposable
	{
		private readonly TaskDbContext _db;
		public DataAccess(ContextFactory contextFactory)
		{
			_db = contextFactory.CreateDbContext(null);
		}
		public void AddComment(long taskId, string comment)
		{
			_db.Comments.Add( new Comment { Content = comment, TaskId = taskId });
		}

		public long AddTask(long userId, TaskDataAccessDTO task)
		{
			//_db.Tasks.Add(task);
			throw new NotImplementedException();
		}

		public void DeleteTask(long taskId)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			_db.Dispose();
		}

		public IEnumerable<TaskDataAccessDTO> GetAllTasksByUserId(long userId)
		{
			throw new NotImplementedException();
		}

		public TaskDataAccessDTO GetTaskById(long taskId)
		{
			throw new NotImplementedException();
		}

		public void UpdateTask(TaskDataAccessDTO task)
		{
			throw new NotImplementedException();
		}
	}
}
