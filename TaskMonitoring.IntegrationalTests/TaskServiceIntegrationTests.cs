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

namespace TaskMonitoring.IntegrationalTests
{
	public class Tests
	{
		private ITaskService _service;
		private IDataAccess _dataAccess;
		const long _userId = 1;
		private TaskDTO _task;
		const long _taskId = 123;

		[SetUp]
		public void Setup()
		{
			_dataAccess = new DataAccess(new ContextFactory());
			_service = new TaskService(_dataAccess);
			_task = new TaskDTO
			{
				Id = _taskId,
				Comments = new List<string> { "comment1" },
				Summary = "summary",
				Title = "title"
			};
		}

		[OneTimeTearDown]
		public void TearDown()
		{
			try
			{
				_service.DeleteTaskById(_taskId);
			}
			catch(Exception)
			{				
			}

		}

		[Test]
		public void CreateNewTaskShouldBeSuccess()
		{
			//arrange
			//actual
			var act = _service.CreateTask(_userId, _task);
			var expectedTask = _dataAccess.GetTaskById(act.Id);

			//assert
			Assert.AreEqual(expectedTask.Id, act.Id);
			Assert.AreEqual(expectedTask.Comments, act.Comments);
			Assert.AreEqual(expectedTask.Summary, act.Summary);
			Assert.AreEqual(expectedTask.Title, act.Title);
			Assert.AreEqual(expectedTask.UserId, act.UserId);
		}

		[Test]
		public void CreateNewTaskForNotExistedUserShouldBeException()
		{
			//arrange
			//actual
			//assert
			Assert.Throws<CannotAddTaskException>(() => _service.CreateTask(11111, _task));
		}

		[Test]
		public void AddCommentForExistedTaskShouldBeSuccess()
		{
			//arrange
			var comment = "comment2";

			//actual
			var actTask = _service.CreateTask(_userId, _task);
			_service.AddComment(_task.Id, comment);

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
			Assert.Throws<CannotAddCommentException>(() => _service.AddComment(111, comment));
		}

		[Test]
		public void UpdateTaskShouldBeSuccess()
		{
			//arrange
			var updatedTask = new TaskDTO
			{
				Id = _taskId,
				Summary = "summary2",
				Title = "title2"
			};

			//actual
			_ =  _service.CreateTask(_userId, _task);
			_service.UpdateTask(updatedTask);

			var expectedTask = Util<TaskDataAccessDTO, TaskDTO>.MapFrom(_dataAccess.GetTaskById(_taskId));

			//assert
			Assert.AreEqual(expectedTask, updatedTask);
		}

		[Test]
		public void DeleteTaskShouldBeSuccess()
		{
			//arrange
			//actual
			var actTask = _service.CreateTask(_userId, _task);
			_service.DeleteTaskById(_taskId);

			//assert
			Assert.Throws<CannotGetTaskException>(() => _dataAccess.GetTaskById(_taskId));
		}
	}
}