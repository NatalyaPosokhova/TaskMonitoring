using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMonitoring.Cards.DataAccess.Interface.Exceptions
{
	public class CannotAddCommentException : DatabaseLayerException
	{
		public CannotAddCommentException(string message, Exception ex = null) : base(message, ex)
		{

		}
	}
}
