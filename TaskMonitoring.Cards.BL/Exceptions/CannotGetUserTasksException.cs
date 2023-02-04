using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMonitoring.Cards.BL.Exceptions
{
	public class CannotGetUserTasksException : Exception
	{
		public CannotGetUserTasksException(string Message, Exception ex = null) : base(Message, ex)
		{

		}
	}
}
