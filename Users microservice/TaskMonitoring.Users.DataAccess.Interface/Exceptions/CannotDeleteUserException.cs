using System;

namespace TaskMonitoring.Users.DataAccess.Interface.Exceptions
{
	public class CannotDeleteUserException : Exception
	{
		public CannotDeleteUserException(string message, Exception ex = null) : base(message, ex)
		{

		}
	}
}
