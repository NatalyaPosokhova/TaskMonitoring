using System;

namespace TaskMonitoring.Cards.DataAccess.Interface.Exceptions
{
	public class CannotAddTaskException : DatabaseLayerException
	{
		public CannotAddTaskException(string message, Exception ex = null) : base(message, ex)
		{

		}
	}
}
