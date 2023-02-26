namespace TaskMonitoring.Timing.BL.Interface.Exceptions
{
	public class TaskNotFoundException : Exception
	{
		public TaskNotFoundException(string message, Exception ex = null) : base(message, ex)
		{
		}
	}
}
