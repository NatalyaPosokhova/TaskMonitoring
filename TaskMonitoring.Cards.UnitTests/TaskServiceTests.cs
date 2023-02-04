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
using System.Threading.Tasks;

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
		public async Task ClearUserTasks()
		{
			var tasks = await _taskService.GetAllTasks(_userId);
			tasks.ToList().ForEach(async task => await _taskService.DeleteTaskById(_userId, task.Id));
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
		public async Task DeleteTaskShouldBeSuccess()
		{
			//arrange
			var taskId = 111;

			//act
			await _taskService.DeleteTaskById(_userId, taskId);

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
			Assert.Throws<BL.Exceptions.TaskNotFoundException>(async () => await _taskService.DeleteTaskById(_userId, taskId));
		}

		[Test]
		public async Task AddCommentToTaskShouldBeSuccessAsync()
		{
			//arrange
			long taskId = 123;
			string comment2 = "comment2";

			//act
			await _taskService.AddComment(_userId, taskId, comment2);

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
			Assert.Throws<BL.Exceptions.TaskNotFoundException>(async () => await _taskService.AddComment(_userId, taskId, "test"));
		}
	}
}