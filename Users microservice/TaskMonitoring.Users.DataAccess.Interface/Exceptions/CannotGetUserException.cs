using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMonitoring.Users.DataAccess.Interface.Exceptions
{
	public class CannotGetUserException : Exception
	{
		public CannotGetUserException(string message, Exception ex = null) : base(message, ex)
		{

		}
	}
}
