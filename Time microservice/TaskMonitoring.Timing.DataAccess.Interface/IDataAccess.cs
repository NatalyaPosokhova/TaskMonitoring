using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMonitoring.Timing.DataAccess.Interface
{
	public interface IDataAccess
	{
		void PutStartTaskTime(long userId, long taskId, DateTime startTime);

		void PutStopTaskTime(long userId, long taskId, DateTime stopTime);
	}
}
