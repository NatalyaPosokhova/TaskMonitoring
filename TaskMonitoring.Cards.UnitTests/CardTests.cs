using NUnit.Framework;
using TaskMonitoring.Cards.BL.Interface;
using TaskMonitoring.Cards.BL;
using TaskMonitoring.Cards.BL.Exceptions;
using TaskMonitoring.Cards.BL.Interface.Enums;

namespace TaskMonitoring.Cards.UnitTests
{
	public class CardTests
	{
		private ICard card;

		[SetUp]
		public void Setup()
		{
			card = new Card("title", "description");
		}

		[Test]
		public void AddTaskOnCardShouldBeSuccess()
		{
			//arrange
			int taskId = 1;
			string taskTitle = "taskTitle";
			string taskDescription = "taskDescription";

			//act
			var task = card.AddTask(taskId, taskTitle, taskDescription);

			//assert
			Assert.AreEqual(card.GetTaskById(taskId), task);
		}

		[Test]
		public void DeleteTaskFromCardShouldBeSuccess()
		{
			//arrange
			int taskId = 1;
			string taskTitle = "taskTitle";
			string taskDescription = "taskDescription";
			var task = card.AddTask(taskId, taskTitle, taskDescription);

			//act
			card.DeleteTask(taskId);

			//assert
			Assert.Throws<CardNotFoundException>(() => card.GetTaskById(taskId));
		}

		[Test]
		public void UpdateTaskOnCardShouldBeSuccess()
		{
			//arrange
			int taskId = 1;
			string taskTitle = "taskTitle";
			string taskDescription = "taskDescription";
			card.AddTask(taskId, taskTitle, taskDescription);

			string expectedTitle = "new taskTitle";
			string expectedDescription = "new taskDescription";

			//act
			var task = card.UpdateTask(taskId, expectedTitle, expectedDescription);

			//assert
			Assert.AreEqual(expectedTitle, task.Title);
			Assert.AreEqual(expectedTitle, task.Description);
		}

		[Test]
		public void ChangeCardStatusShouldBeSuccess()
		{
			//arrange
			Statuses expectedStatus = Statuses.Active;
			//act
			card.ChangeStatus(expectedStatus);
			//assert
			Assert.AreEqual(expectedStatus, card.Status);
		}
	}
}