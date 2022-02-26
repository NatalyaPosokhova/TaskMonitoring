using TaskMonitoring.Cards.BL.Interface.Enums;

namespace TaskMonitoring.Cards.BL.Interface
{
	public interface ICard
	{
		public Statuses Status { get; }
		public void ChangeDescription(string newDescription);
		public void ChangeTitle(string newTitle);
		public void ChangeStatus(Statuses newStatus);
		public ITask AddTask(int taskId, string taskTitle, string taskDescription);
		public void DeleteTask(int taskId);
		public ITask UpdateTask(int taskId, string taskTitle = null, string taskDescription = null);
		public ITask GetTaskById(int taskId);
	}
}
