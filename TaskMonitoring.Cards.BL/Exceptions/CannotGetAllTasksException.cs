using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMonitoring.Cards.BL.Exceptions
{
	public class CannotGetAllTasksException : Exception
	{
		public CannotGetAllTasksException(string Message, Exception ex) : base(Message, ex)
		{

		}
	}
}
