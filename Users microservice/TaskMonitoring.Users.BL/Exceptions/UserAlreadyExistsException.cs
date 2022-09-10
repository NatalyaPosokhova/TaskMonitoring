using System;

namespace TaskMonitoring.Users.BL.Exceptions
{
	public class UserAlreadyExistsException : Exception
	{
		public UserAlreadyExistsException(string message, Exception ex = null) : base(message, ex)
		{

		}
	}
}
