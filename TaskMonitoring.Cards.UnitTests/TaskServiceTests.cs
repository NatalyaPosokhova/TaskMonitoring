using NUnit.Framework;
using TaskMonitoring.Cards.BL.Interface;
using TaskMonitoring.Cards.BL;
using TaskMonitoring.Cards.BL.Exceptions;
using TaskMonitoring.Cards.BL.Interface.Enums;
using TaskMonitoring.Cards.BL.Interface.DTO;
using System.Linq;
using System.Collections.Generic;

namespace TaskMonitoring.Cards.UnitTests
{
	public class TaskServiceTests
	{
		private ITaskService _taskService;
		long _userId = 123;

		[SetUp]
		public void Setup()
		{
			_taskService = new TaskService();
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

			//act
			var actTask = _taskService.CreateTask(_userId, expTask);

			//assert
			Assert.IsNotNull(actTask);
		}

		[Test]
		public void DeleteTaskShouldBeSuccess()
		{
			//TODO: вот тут если ещё будет тест с созданием задачи для данного пользователя,
			//то этот тест не пройдёт.
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

			//assert
			Assert.IsNull(_taskService.GetAllTasks(_userId));
		}

		[Test]
		public void DeleteNotExistedTaskShouldBeException()
		{
			//arrange
			int taskId = 77;

			//act
			//assert
			Assert.Throws<TaskNotFoundException>(() => _taskService.DeleteTaskById(taskId));
		}

		[Test]
		public void SaveTaskShouldBeSuccess()
		{
			//arrange
			string expectedTitle = "345";
			Task expTask = new Task
			{
				Comments = new List<string> { "comment1" },
				Summary = "summary",
				Title = "title"
			};
			Task actTask = _taskService.CreateTask(_userId, expTask);

			//act
			actTask.Title = expectedTitle;
			_taskService.SaveTask(actTask);

			//assert
			Assert.AreEqual(expectedTitle, _taskService.GetAllTasks(_userId).First().Title);
		}



	}
}