using System;

namespace TaskMonitoring.Users.BL.Exceptions
{
	public class UserNotFoundException : Exception
	{
		public UserNotFoundException(string message, Exception ex = null) : base(message, ex)
		{

		}
	}
}
