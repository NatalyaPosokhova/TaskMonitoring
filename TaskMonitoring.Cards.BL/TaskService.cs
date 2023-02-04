using System.Collections.Generic;
using TaskMonitoring.Cards.BL.Interface.DTO;
using TaskMonitoring.Cards.BL.Interface;
using TaskMonitoring.Cards.DataAccess.Interface;
using TaskMonitoring.Cards.DataAccess.Interface.Exceptions;
using TaskMonitoring.Cards.BL.Exceptions;
using System.Linq;
using TaskMonitoring.Utilities;
using TaskMonitoring.APIClients.Users.Interfaces;
using System.Threading.Tasks;
using System;

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

		public async Task AddComment(long userId, long taskId, string comment)
		{
			try
			{
				var user = await _webAPIUsers.GetUserById(userId);
				if(user == null)
					throw new UserNotFoundException($"Пользователя с id = {userId} не существует.");

				_data.AddComment(taskId, comment);
			}
			catch (CannotAddCommentException ex)
			{
				throw new Exceptions.TaskNotFoundException("Task is absent in database.", ex);
			}
		}

		public async Task<TaskDTO> CreateTask(long userId, TaskDTO task)
		{
			try
			{
				var user = await CheckIfUserExists(userId);
				task.UserId = user.Id;
				AddDefaultComment(task);

				var mappedTask = Util<TaskDTO, TaskDataAccessDTO>.Map(task);
				var taskId = _data.AddTask(mappedTask);
				task.Id = taskId;

					return task;
			}
			catch(Exception ex)
			{
				throw new CannotCreateTaskException("Не удалось создать задачу.", ex);
			}

		}

		private static void AddDefaultComment(TaskDTO task)
		{
			var comment = $"Задача создана {DateTime.Now}";
			task.Comments ??= new List<string>();
			task.Comments.Add(comment);
		}

		public async Task DeleteTaskById(long userId, long taskId)
		{
			try
			{
				await CheckIfUserExists(userId);
				_data?.DeleteTask(taskId);
			}
			catch (CannotDeleteTaskException ex)
			{
				throw new Exceptions.TaskNotFoundException("Task is absent in database.", ex);
			}
		}

		public async Task<IEnumerable<TaskDTO>> GetAllTasks(long userId)
		{
			try
			{
				await CheckIfUserExists(userId);
				return _data?.GetAllTasksByUserId(userId)?.Select(task => Util<TaskDataAccessDTO, TaskDTO>.Map(task));
			}
			catch(Exception ex)
			{
				throw new CannotGetUserTasksException(ex.Message, ex);
			}
		}

		public async Task UpdateTask(long userId, TaskDTO task)
		{
			try
			{
				await CheckIfUserExists(userId);
				var mappedTask = Util<TaskDTO, TaskDataAccessDTO>.Map(task);
				_data.UpdateTask(mappedTask);
			}
			catch(Exception ex)
			{
				throw new Exceptions.CannotUpdateTaskException(ex.Message, ex);
			}
		}

		private async Task<APIClients.Users.Interfaces.DTO.User> CheckIfUserExists(long userId)
		{			
			var user = await _webAPIUsers.GetUserById(userId);
			if(user == null)
				throw new UserNotFoundException($"Пользователя с id = {userId} не существует.");

			return user;
		}
	}
}
