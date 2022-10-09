using System.Collections.Generic;
using TaskMonitoring.Cards.BL.Interface.DTO;
using TaskMonitoring.Cards.BL.Interface;
using TaskMonitoring.Cards.DataAccess.Interface;
using TaskMonitoring.Cards.DataAccess.Interface.Exceptions;
using TaskMonitoring.Cards.BL.Exceptions;
using System.Linq;
using TaskMonitoring.Utilities;
using TaskMonitoring.APIClients.Users.Interfaces;

namespace TaskMonitoring.Cards.BL
{
	public class TaskService : ITaskService
	{
		private IDataAccess _data;
		private readonly IWebAPIUsers _webAPIUsers;

		public TaskService(IDataAccess data, IWebAPIUsers webAPIUsers)
		{
			_data = data;
			_webAPIUsers = webAPIUsers;
		}

		public void AddComment(long taskId, string comment)
		{
			try
			{
				_data.AddComment(taskId, comment);
			}
			catch (CannotAddCommentException ex)
			{
				throw new Exceptions.TaskNotFoundException("Task is absent in database.", ex);
			}
		}

		public TaskDTO CreateTask(long userId, TaskDTO task)
		{
			try
			{
				if(_webAPIUsers.GetUserById(userId) != null)
				{
					task.UserId = userId;
					var mappedTask = Util<TaskDTO, TaskDataAccessDTO>.Map(task);
					var taskId = _data.AddTask(mappedTask);
					task.Id = taskId;
					return task;
				}
				return null;
			}
			catch(System.Exception ex)
			{
				throw new CannotCreateTaskException("Не удалось создать задачу.", ex);
			}

		}

		public void DeleteTaskById(long taskId)
		{
			try
			{
				_data?.DeleteTask(taskId);
			}
			catch (CannotDeleteTaskException ex)
			{
				throw new Exceptions.TaskNotFoundException("Task is absent in database.", ex);
			}
		}

		public IEnumerable<TaskDTO> GetAllTasks(long userId)
		{
			return _data?.GetAllTasksByUserId(userId)?.Select(task => Util<TaskDataAccessDTO, TaskDTO>.Map(task));
		}

		public void UpdateTask(TaskDTO task)
		{
			var mappedTask = Util<TaskDTO, TaskDataAccessDTO>.Map(task);
			_data.UpdateTask(mappedTask);			
		}
	}
}
