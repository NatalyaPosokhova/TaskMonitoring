using System;

namespace TaskMonitoring.Users.DataAccess.Interface.Exceptions
{
	public class UserAlreadyExistsException : Exception
	{
		public UserAlreadyExistsException(string message, Exception ex = null) : base(message, ex)
		{

		}
	}
}
