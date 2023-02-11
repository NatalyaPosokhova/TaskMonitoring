using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMonitoring.Cards.BL.Exceptions
{
	public class CannotAddCommentException : Exception
	{
		public CannotAddCommentException(string Message, Exception ex) : base(Message, ex)
		{

		}
	}
}
