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
			TaskDataAccessDTO expTaskDataAccess = new TaskDataAccessDTO
			{ 
				Comments =new List<string>{ "comment1" },
				Summary = "summary",
				Title = "title"
			};

			TaskDTO expTask = new TaskDTO
			{
				Comments = new List<string> { "comment1" },
				Summary = "summary",
				Title = "title"
			};

			long expectedTaskId = 1;
			_dataAccess.AddTask(_userId, expTaskDataAccess).Returns(expectedTaskId);

			//act
			var actTask = _taskService.CreateTask(_userId, expTask);

			//assert
			Assert.AreEqual(expectedTaskId, actTask.Id);
		}

		[Test]
		public void DeleteTaskShouldBeSuccess()
		{
			//arrange
			var taskId = 111;

			//act
			_taskService.DeleteTaskById(taskId);

			//assert
			_dataAccess.Received().DeleteTask(taskId);
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
			Assert.Throws<TaskNotFoundException>(() => _taskService.DeleteTaskById(taskId));
		}

		[Test]
		public void AddCommentToTaskShouldBeSuccess()
		{
			//arrange
			TaskDTO task = new TaskDTO
			{
				Comments = new List<string> { "comment1" },
				Summary = "summary",
				Title = "title"
			};

			TaskDataAccessDTO taskDataAccess = new TaskDataAccessDTO
			{
				Comments = new List<string> { "comment1", "comment2" },
				Summary = "summary",
				Title = "title"
			};

			long taskId = 123;
			string comment2 = "comment2";
			_dataAccess.AddTask(_userId, taskDataAccess).Returns(taskId);
			_taskService.CreateTask(_userId, task);

			//act
			_taskService.AddComment(taskId, comment2);
			_dataAccess.Received().AddComment(taskId, comment2);

			//assert
			_dataAccess.GetAllTasksByUserId(_userId).Returns(new List<TaskDataAccessDTO> { taskDataAccess });
			Assert.IsTrue(_taskService.GetAllTasks(_userId)?.First()?.Comments?.Any(comment => comment == comment2) ?? false);
		}

		[Test]
		public void AddCommentToNotExistedTaskShouldBeException()
		{
			//arrange
			long taskId = 12;
			_dataAccess.When(x => x.AddComment(taskId, "test"))
				.Do(x => { throw new CannotAddCommentException(""); });

			//act
			//assert
			Assert.Throws<TaskNotFoundException>(() => _taskService.AddComment(taskId, "test"));
		}
	}
}