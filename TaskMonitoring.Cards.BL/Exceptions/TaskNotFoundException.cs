using System;

namespace TaskMonitoring.Cards.BL.Exceptions
{
	public class TaskNotFoundException : Exception
	{
		public TaskNotFoundException(string Message, Exception ex) : base(Message, ex)
		{

		}
	}
}
