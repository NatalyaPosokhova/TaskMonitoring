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
		public void ClearUserTasks()
		{
			var tasks =_taskService.GetAllTasks(_userId);
			tasks.ToList().ForEach(task => _taskService.DeleteTaskById(_userId, task.Id));
		}

		[Test]
		public void CreateTaskShouldBeSuccess()
		{
			//arrange
			TaskDataAccessDTO expTaskDataAccess = new TaskDataAccessDTO
			{ 
				Comments =new List<string>{ "comment1"},
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
			_webAPIUsers.GetUserById(_userId).Returns<User>(new User());

			//act
			_taskService.DeleteTaskById(_userId, taskId);

			//assert
			_dataAccess.Received().DeleteTask(taskId);
		}

		[Test]
		public void DeleteNotExistedTaskShouldBeException()
		{
			//arrange
			int taskId = 77;
			_dataAccess.When(x => x.DeleteTask(taskId))
				.Do(x => {throw new DataAccess.Interface.Exceptions.CannotDeleteTaskException("");});
			_webAPIUsers.GetUserById(_userId).Returns<User>(new User());

			//act
			//assert
			Assert.Throws<BL.Exceptions.TaskNotFoundException>(() => _taskService.DeleteTaskById(_userId, taskId));
		}

		[Test]
		public void AddCommentToTaskShouldBeSuccessAsync()
		{
			//arrange
			long taskId = 123;
			string comment2 = "comment2";
			_webAPIUsers.GetUserById(_userId).Returns<User>(new User());

			//act
			_taskService.AddComment(_userId, taskId, comment2);

			//assert
			_dataAccess.Received().AddComment(taskId, comment2);
		}

		[Test]
		public void AddCommentToNotExistedTaskShouldBeException()
		{
			//arrange
			long taskId = 12;
			_dataAccess.When(x => x.AddComment(taskId, "test"))
				.Do(x => { throw new DataAccess.Interface.Exceptions.CannotAddCommentException(""); });
			_webAPIUsers.GetUserById(_userId).Returns<User>(new User());

			//act
			//assert
			Assert.Throws<BL.Exceptions.TaskNotFoundException>(() =>  _taskService.AddComment(_userId, taskId, "test"));
		}

		[Test]
		public async Task GetTaskByIdShouldBeSuccess()
		{
			//arrange
			long taskId = 123;
			var comment = "comment1";
			var summary = "summary";
			var title = "title";
			TaskDataAccessDTO task = new TaskDataAccessDTO
			{
				Comments = new List<string> { comment },
				UserId = _userId,
				Summary = summary,
				Title = title,
				Id = taskId
			};

			TaskDTO expectedTask = new TaskDTO
			{
				Comments = new List<string> { comment },
				UserId = _userId,
				Summary = summary,
				Title = title,
				Id = taskId
			};
			_webAPIUsers.GetUserById(_userId).Returns<User>(new User());
			_dataAccess.GetTaskById(taskId).Returns(task);

			//act
			var actTask = _taskService.GetTaskById(_userId, task.Id);

			//assert
			Assert.AreEqual(expectedTask, actTask);
		}

	}
}