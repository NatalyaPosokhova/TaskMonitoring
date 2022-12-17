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
using TaskMonitoring.APIClients.Users.Interfaces;
using TaskMonitoring.APIClients.Users.Interfaces.DTO;

namespace TaskMonitoring.Cards.UnitTests
{
	public class TaskServiceTests
	{
		private ITaskService _taskService;
		private IDataAccess _dataAccess;
		private  IWebAPIUsers _webAPIUsers;

		long _userId = 123;

		[SetUp]
		public void Setup()
		{
			_dataAccess = Substitute.For<IDataAccess>();
			_webAPIUsers = Substitute.For<IWebAPIUsers>();
			_taskService = new TaskService(_dataAccess, _webAPIUsers);
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
				Title = "title",
				UserId = _userId
			};

			TaskDTO expTask = new TaskDTO
			{
				Comments = new List<string> { "comment1" },
				Summary = "summary",
				Title = "title"
			};

			var user = new User
			{
				Id = _userId
			};

			long expectedTaskId = 1;
			_dataAccess.AddTask( expTaskDataAccess).Returns(expectedTaskId);
			_webAPIUsers.GetUserById(_userId).Returns(user);

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
			Assert.Throws<BL.Exceptions.TaskNotFoundException>(() => _taskService.DeleteTaskById(taskId));
		}

		[Test]
		public void AddCommentToTaskShouldBeSuccess()
		{
			//arrange
			long taskId = 123;
			string comment2 = "comment2";

			//act
			_taskService.AddComment(taskId, comment2);

			//assert
			_dataAccess.Received().AddComment(taskId, comment2);
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
			Assert.Throws<BL.Exceptions.TaskNotFoundException>(() => _taskService.AddComment(taskId, "test"));
		}
	}
}