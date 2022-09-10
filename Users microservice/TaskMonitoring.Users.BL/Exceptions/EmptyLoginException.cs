using System;

namespace TaskMonitoring.Users.BL.Exceptions
{
	public class EmptyLoginException : Exception
	{
		public EmptyLoginException(string message, Exception ex = null) : base(message, ex)
		{

		}
	}
}
