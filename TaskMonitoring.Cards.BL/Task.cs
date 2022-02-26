using System;
using TaskMonitoring.Cards.BL.Interface;

namespace TaskMonitoring.Cards.BL
{
	public class Task : ITask
	{
		public string Title { get; private set; }
		public string Description { get; private set; }
		private int _id;
		public Task(int id, string title, string description)
		{
			_id = id;
			Title = title;
			Description = description;
		}

		public void ChangeTaskDescription(string newDescription)
		{
			throw new NotImplementedException();
		}

		public void ChangeTaskTitle(string newTitle)
		{
			throw new NotImplementedException();
		}

		public override bool Equals(object task1)
		{
			//Check for null and compare run-time types.
			if ((task1 == null) || !this.GetType().Equals(task1.GetType()))
			{
				return false;
			}
			else
			{
				Task t = (Task)task1;
				return (_id == t._id) && (Title == t.Title) && (Description == t.Description);
			}
		}
	}
}
