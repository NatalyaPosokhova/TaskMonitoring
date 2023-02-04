using System;

namespace TaskMonitoring.Cards.BL.Exceptions
{
	public class UserNotFoundException : Exception
	{
		public UserNotFoundException(string Message, Exception ex = null) : base(Message, ex)
		{

		}
	}
}
