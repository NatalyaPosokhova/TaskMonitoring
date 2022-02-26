namespace TaskMonitoring.Cards.BL.Interface
{
	public interface ITask
	{
		public string Title { get; }
		public string Description { get; }
		public void ChangeTaskDescription(string newDescription);
		public void ChangeTaskTitle(string newTitle);
	}
}
