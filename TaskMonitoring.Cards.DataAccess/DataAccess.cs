using System;
using System.Collections.Generic;
using TaskMonitoring.Cards.DataAccess.Interface;
using TaskMonitoring.Cards.DataAccess.Interface.Exceptions;
using TaskMonitoring.Cards.DataAccess.Models;
using TaskMonitoring.Utilities;

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
			try
			{
				_db.Comments.Add(new Comment { Content = comment, TaskId = taskId });
				_db.SaveChanges();
			}
			catch(Exception e)
			{
				throw new CannotAddCommentException($"Cannot add comment {e.Message}");
			}
		}

		public long AddTask(TaskDataAccessDTO task)
		{
			try
			{
				var mappedTask = Util<TaskDataAccessDTO, Task>.MapFrom(task);
				_db.Tasks.Add(mappedTask);
				_db.SaveChanges();
				return mappedTask.Id;
			}
			catch(Exception e)
			{
				throw new CannotAddTaskException(e.Message);
			}
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
