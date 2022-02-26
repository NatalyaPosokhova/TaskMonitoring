using System;

namespace TaskMonitoring.Cards.BL.Exceptions
{
	public class CardNotFoundException : Exception
	{
		public CardNotFoundException(string Message, Exception ex) : base(Message, ex)
		{

		}
	}
}
