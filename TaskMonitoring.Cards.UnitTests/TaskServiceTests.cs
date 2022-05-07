using NUnit.Framework;
using TaskMonitoring.Cards.BL.Interface;
using TaskMonitoring.Cards.BL;
using TaskMonitoring.Cards.BL.Exceptions;
using TaskMonitoring.Cards.BL.Interface.Enums;
using TaskMonitoring.Cards.BL.Interface.DTO;
using System.Linq;
using System.Collections.Generic;
using TaskMonitoring.Cards.DataAccess.Interface;
using NSubstitute;
using TaskMonitoring.Cards.DataAccess.Interface.Exceptions;

namespace TaskMonitoring.Cards.UnitTests
{
	public class TaskServiceTests
	{
		private ITaskService _taskService;
		private IDataAccess _dataAccess;
		
		long _userId = 123;

		[SetUp]
		public void Setup()
		{
			_dataAccess = Substitute.For<IDataAccess>();
			_taskService = new TaskService(_dataAccess);
		}

		[TearDown]
		public void ClearUserTasks()
		{
			var tasks = _taskService.GetAllTasks(_userId);
			tasks.ToList().ForEach(task => _taskService.DeleteTaskById(task.Id));
		}

		[Test]
		public void CreateTaskShouldBeSuccess()
		{
			//arrange
			Task expTask = new Task 
			{ 
				Comments =new List<string>{ "comment1" },
				Summary = "summary",
				Title = "title"
			};

			long expectedTaskId = 1;
			_dataAccess.AddTask(_userId, expTask).Returns(expectedTaskId);

			//act
			var actTask = _taskService.CreateTask(_userId, expTask);

			//assert
			Assert.AreEqual(expectedTaskId, actTask.Id);
		}

		[Test]
		public void DeleteTaskShouldBeSuccess()
		{
			//arrange
			Task expTask = new Task
			{
				Comments = new List<string> { "comment1" },
				Summary = "summary",
				Title = "title"
			};

			var actTask = _taskService.CreateTask(_userId, expTask);

			//act
			_taskService.DeleteTaskById(actTask.Id);
			_dataAccess.Received().DeleteTask(actTask.Id);

			//assert
			_dataAccess.GetAllTasksByUserId(_userId).Returns(new List<Task> {});
			Assert.IsEmpty(_taskService.GetAllTasks(_userId));
		}

		[Test]
		public void DeleteNotExistedTaskShouldBeException()
		{
			//arrange
			int taskId = 77;
			_dataAccess.When(x => x.DeleteTask(taskId))
				.Do(x => {throw new CannotDeleteTaskException("");});

			//act
			//assert
			//_dataAccess.Received().DeleteTask(taskId);
			Assert.Throws<TaskNotFoundException>(() => _taskService.DeleteTaskById(taskId));
		}
	}
}