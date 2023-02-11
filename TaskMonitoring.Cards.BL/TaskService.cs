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

		public void AddComment(long userId, long taskId, string comment)
		{
			try
			{
				if(!CheckIfUserExists(userId, out var _))
					throw new UserNotFoundException($"Пользователя с id = {userId} не существует.");

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
				if(!CheckIfUserExists(userId, out var actualUser))
					throw new UserNotFoundException($"Пользователя с id = {userId} не существует.");
				task.UserId = actualUser.Id;
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

		public void DeleteTaskById(long userId, long taskId)
		{
			try
			{
				if(!CheckIfUserExists(userId, out var _))
					throw new UserNotFoundException($"Пользователя с id = {userId} не существует.");
				_data?.DeleteTask(taskId);
			}
			catch (CannotDeleteTaskException ex)
			{
				throw new Exceptions.TaskNotFoundException("Task is absent in database.", ex);
			}
		}

		public IEnumerable<TaskDTO> GetAllTasks(long userId)
		{
			try
			{
				if(!CheckIfUserExists(userId, out var _))
					throw new UserNotFoundException($"Пользователя с id = {userId} не существует.");

				return _data?.GetAllTasksByUserId(userId)?.Select(task => Util<TaskDataAccessDTO, TaskDTO>.Map(task));
			}
			catch(Exception ex)
			{
				throw new CannotGetUserTasksException(ex.Message, ex);
			}
		}

		public void UpdateTask(long userId, TaskDTO task)
		{
			try
			{
				if(!CheckIfUserExists(userId, out var _))
					throw new UserNotFoundException($"Пользователя с id = {userId} не существует.");

				var mappedTask = Util<TaskDTO, TaskDataAccessDTO>.Map(task);
				_data.UpdateTask(mappedTask);
			}
			catch(Exception ex)
			{
				throw new Exceptions.CannotUpdateTaskException(ex.Message, ex);
			}
		}
		public TaskDTO GetTaskById(long userId, long taskId)
		{
			try
			{
				if(!CheckIfUserExists(userId, out var _))
					throw new UserNotFoundException($"Пользователя с id = {userId} не существует.");

				var task = _data.GetTaskById(taskId);
				return Util<TaskDataAccessDTO, TaskDTO>.Map(task);
			}
			catch(Exception ex)
			{
				throw new Exceptions.CannotGetTaskByIdException(ex.Message, ex);
			}
		}

		private bool CheckIfUserExists(long userId, out UserDTO actualUser)
		{
			actualUser = default;
			var user = _webAPIUsers.GetUserById(userId).Result;
			if(user == null)
				return false;

			actualUser = Util<APIClients.Users.Interfaces.DTO.User, UserDTO>.Map(user);
			return true;
		}
		private static void AddDefaultComment(TaskDTO task)
		{
			var comment = $"Задача создана {DateTime.Now}";
			task.Comments ??= new List<string>();
			task.Comments.Add(comment);
		}

	}
}
