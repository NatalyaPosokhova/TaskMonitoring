using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMonitoring.Timing.DataAccess.Interface;

namespace TaskMonitoring.Timing.DataAccess
{
	public class DataAccess : IDataAccess
	{
		public DateTime? GetStartTimeForTask(long userId, long taskId)
		{
			throw new NotImplementedException();
		}

		public void PutStartTaskTime(long userId, long taskId, DateTime startTime)
		{
			throw new NotImplementedException();
		}

		public void PutStopTaskTime(long userId, long taskId, DateTime stopTime)
		{
			throw new NotImplementedException();
		}
	}
}
