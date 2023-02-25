using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMonitoring.Timing.BL.Interface.Exceptions
{
	public class CannotLaunchTimerForTaskException : Exception
	{
		public CannotLaunchTimerForTaskException(string message, Exception ex = null) : base(message, ex)
		{
		}
	}
}
