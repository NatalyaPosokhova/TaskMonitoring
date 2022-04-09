using System;
using System.Collections.Generic;
using TaskMonitoring.Cards.BL.Interface;
using TaskMonitoring.Cards.BL.Interface.Enums;

namespace TaskMonitoring.Cards.BL
{
	public class Card : ICard
	{
		public string Title { get; private set; }
		public string Description { get; private set; }
		public Statuses Status { get; private set; }
		private List<ICardTask> _tasks;

		public Card(string title, string description)
		{
			Title = title;
			Description = description;
			Status = Statuses.Propose;
			_tasks = new List<ICardTask>();
		}
		public void ChangeDescription(string newDescription)
		{
			throw new NotImplementedException();
		}

		public void ChangeTitle(string newTitle)
		{
			throw new NotImplementedException();
		}

		public void ChangeStatus(Statuses newStatus)
		{
			throw new NotImplementedException();
		}

		public ICardTask AddTask(int taskId, string taskTitle, string taskDescription)
		{
			throw new NotImplementedException();
		}

		public void DeleteTask(int taskId)
		{
			throw new NotImplementedException();
		}

		public ICardTask UpdateTask(int taskId, string taskTitle = null, string taskDescription = null)
		{
			throw new NotImplementedException();
		}

		public ICardTask GetTaskById(int taskId)
		{
			throw new NotImplementedException();
		}
	}
}
