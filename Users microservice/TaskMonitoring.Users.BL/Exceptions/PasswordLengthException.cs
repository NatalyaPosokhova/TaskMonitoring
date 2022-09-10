using System;

namespace TaskMonitoring.Users.BL.Exceptions
{
	public class PasswordLengthException : Exception
	{
		public PasswordLengthException(string message, Exception ex = null) : base(message, ex)
		{

		}
	}
}
