using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskMonitoring.Cards.DataAccess.Helper;
using TaskMonitoring.Cards.DataAccess.Interface;
using TaskMonitoring.Cards.DataAccess.Interface.Exceptions;
using TaskMonitoring.Cards.DataAccess.Models;
using TaskMonitoring.Utilities;

namespace TaskMonitoring.Cards.DataAccess
{
	public class DataAccess : IDataAccess, IDisposable
	{
		private readonly TaskDbContext _db;
		public DataAccess(ContextFactory<TaskDbContext> contextFactory)
		{
			_db = contextFactory.CreateDbContext(null);
		}

		public DataAccess(TaskDbContext db)
		{
			_db = db;
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
				throw new CannotAddCommentException($"Cannot add comment {e.Message}, {e}");
			}
		}

		public long AddTask(TaskDataAccessDTO task)
		{
			try
			{
				var mappedTask = Util<TaskDataAccessDTO, Task>.Map(task, mappingExpr => mappingExpr
									.ForMember(dest => dest.Comments, m => m.MapFrom<String2CommentResolver>())
									.ForMember(dest => dest.User, m => m.MapFrom<UserId2UserResolver>()));

				_db.Tasks.Add(mappedTask);
				_db.SaveChanges();
				return mappedTask.Id;
			}
			catch(Exception e)
			{
				throw new CannotAddTaskException(e.Message, e);
			}
		}

		public void DeleteTask(long taskId)
		{
			try
			{
				var task = _db.Tasks.Where(t => t.Id == taskId).FirstOrDefault();
				if(task == null)
				{
					throw new CannotDeleteTaskException($"Task with taskId: {taskId} is absent in database.");
				}
				_db.Tasks.Remove(task);
				_db.SaveChanges();
			}
			catch(Exception e)
			{
				throw new CannotDeleteTaskException(e.Message, e);
			}
		}

		public void Dispose()
		{
			_db.Dispose();
		}

		public IEnumerable<TaskDataAccessDTO> GetAllTasksByUserId(long userId)
		{
			try
			{
				return _db.Tasks.
					Where(t => t.User.Id == userId).
					Select(t => Util<Task, TaskDataAccessDTO>.Map(t, null)).
					ToList();
			}
			catch(Exception e)
			{
				throw new CannotGetTasksException(e.Message, e);
			}
		}

		public TaskDataAccessDTO GetTaskById(long taskId)
		{
			try
			{
				var task = _db.Tasks.Where(t => t.Id == taskId).FirstOrDefault();
				if(task == null)
				{
					throw new ArgumentException($"Task with id {taskId} not found.");
				}
				var result = Util<Task, TaskDataAccessDTO>.Map(task, mappingExpr => mappingExpr
								.ForMember(dest => dest.Comments, m => m.MapFrom<Comment2StringResolver>())
								.ForMember(dest => dest.UserId, m => m.MapFrom<User2UserIdResolver>()));
				return result;
			}
			catch(Exception e)
			{
				throw new CannotGetTaskException(e.Message, e);
			}
		}

		public void UpdateTask(TaskDataAccessDTO task)
		{
			try
			{
				var oldTask = _db.Tasks.Where(t => t.Id == task.Id).FirstOrDefault();
				if(task == null)
				{
					throw new TaskNotFoundException("Task Not Found Exception");
				}
				

				oldTask.Title = task.Title;
				oldTask.Summary = task.Summary;
				_db.SaveChanges();
			}
			catch(Exception e)
			{
				throw new CannotUpdateTaskException(e.Message, e);
			}
		}

	}
}
