﻿using System;
using System.Collections.Generic;
using TaskMonitoring.Cards.BL.Interface.DTO;
using TaskMonitoring.Cards.BL.Interface;
using TaskMonitoring.Cards.DataAccess.Interface;
using TaskMonitoring.Cards.DataAccess.Interface.Exceptions;
using TaskMonitoring.Cards.BL.Exceptions;

namespace TaskMonitoring.Cards.BL
{
	public class TaskService : ITaskService
	{
		private IDataAccess _data;
		public TaskService(IDataAccess data)
		{
			_data = data;
		}

		public void AddComment(long taskId, string comment)
		{
			try
			{
				_data.AddComment(taskId, comment);
			}
			catch (CannotAddCommentException ex)
			{
				throw new TaskNotFoundException("Task is absent in database.", ex);
			}
		}

		public Task CreateTask(long userId, Task task)
		{
			var taskId = _data.AddTask(userId, task);
			task.Id = taskId;
			return task;
		}

		public void DeleteTaskById(long taskId)
		{
			try
			{
				_data.DeleteTask(taskId);
			}
			catch (CannotDeleteTaskException ex)
			{
				throw new TaskNotFoundException("Task is absent in database.", ex);
			}
		}

		public IEnumerable<Task> GetAllTasks(long userId)
		{
			var test = _data.GetAllTasksByUserId(userId);
			return _data.GetAllTasksByUserId(userId);
		}
	}
}
