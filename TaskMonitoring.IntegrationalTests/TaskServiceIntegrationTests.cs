using NUnit.Framework;
using System.Collections.Generic;
using TaskMonitoring.Cards.BL;
using TaskMonitoring.Cards.BL.Interface;
using TaskMonitoring.Cards.BL.Interface.DTO;
using TaskMonitoring.Cards.DataAccess;
using TaskMonitoring.Cards.DataAccess.Interface;
using TaskMonitoring.Cards.DataAccess.Interface.Exceptions;
using TaskMonitoring.Utilities;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TaskMonitoring.APIClients.Users.Interfaces;
using System.Threading.Tasks;

namespace TaskMonitoring.IntegrationalTests
{
	public class Tests
	{
		private ITaskService _service;
		private IDataAccess _dataAccess;
		private IWebAPIUsers _webAPIUsers;
		private long _userId;
		long _taskId = 123;
		private TaskDbContext _db;
		TaskDTO _task;

		[SetUp]
		public async Task SetUp()
		{
			_db = new ContextFactory<TaskDbContext>().CreateDbContext(null);
			_dataAccess = new DataAccess(_db);
			_webAPIUsers = new WebAPIUsers();
			_service = new TaskService(_dataAccess, _webAPIUsers);
			_task = new TaskDTO
			{
				Id = _taskId,
				Comments = new List<string> { "comment1" },
				Summary = "summary",
				Title = "title"
			};

			var user = await _webAPIUsers.CreateUser("login", "password");
			_userId = user.Id;
		}

		[OneTimeTearDown]
		public async Task OneTimeTearDown()
		{
			await _webAPIUsers.DeleteUser(_userId);
		}

		[TearDown]
		public void TearDown()
		{
			_db.Database.ExecuteSqlInterpolated($"DELETE FROM \"Tasks\" Where \"Id\" = {_taskId}");
			//_db.Database.ExecuteSqlInterpolated($"DELETE FROM \"Users\" Where \"Id\" = {_userId}");
		}

		[Test]
		public async Task CreateNewTaskShouldBeSuccess()
		{
			//arrange
			//actual
			var actTask = await _service.CreateTask(_userId, _task);
			var expectedTask = Util<TaskDataAccessDTO,TaskDTO>.Map(_dataAccess.GetTaskById(actTask.Id));

			//assert
			Assert.AreEqual(expectedTask, actTask);
		}

		[Test]
		public async Task AddCommentForExistedTaskShouldBeSuccess()
		{
			//arrange
			var comment = "comment2";

			//actual
			var actTask = await _service.CreateTask(_userId, _task);
			await _service.AddComment(_userId, _task.Id, comment);

			var actComments = _dataAccess.GetTaskById(actTask.Id).Comments;

			//assert
			Assert.IsTrue(actComments.Contains(comment));
		}

		[Test]
		public void AddCommentForNotExistedTaskShouldBeException()
		{
			//arrange
			var comment = "comment1";

			//actual
			//assert
			Assert.Throws<Cards.BL.Exceptions.TaskNotFoundException>(async () => await _service.AddComment(_userId, 111, comment));
		}

		[Test]
		//[Ignore("Temporary off")]
		public async Task UpdateTaskShouldBeSuccess()
		{
			//arrange
			var updatedTask = new TaskDTO
			{
				Id = _taskId,
				Summary = "summary2",
				Title = "title2",
				Comments = _task.Comments,
				UserId = _task.UserId
			};

			//actual
			_ = await _service.CreateTask(_userId, _task);
			await _service.UpdateTask(_userId, updatedTask);

			var expectedTask = Util<TaskDataAccessDTO, TaskDTO>.Map(_dataAccess.GetTaskById(_taskId));

			//assert
			Assert.AreEqual(expectedTask, updatedTask);
		}

		[Test]
		public async Task DeleteTaskShouldBeSuccess()
		{
			//arrange
			//actual
			var actTask = await _service.CreateTask(_userId, _task);
			await _service.DeleteTaskById(_userId, _taskId);

			//assert
			Assert.Throws<CannotGetTaskException>(() => _dataAccess.GetTaskById(_taskId), "Task with id 123 not found.");
		}
	}
}