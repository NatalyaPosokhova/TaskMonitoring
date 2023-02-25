using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMonitoring.APIClients.Tasks;
using TaskMonitoring.Timing.BL.Interface;
using TaskMonitoring.Timing.DataAccess.Interface;

namespace TaskMonitoring.Timing.BL
{
	public class TimingService : ITimingService
	{
		private readonly IDataAccess _dataAccess;
		private readonly IWebAPITasks _tasksProxy;
		public TimingService(IDataAccess dataAccess, IWebAPITasks tasksProxy)
		{
			_dataAccess = dataAccess;
			_tasksProxy = tasksProxy;
		}
		public IDictionary<long, IEnumerable<TimeSpan>> GetReport(long userId, IEnumerable<long> tasks)
		{
			throw new NotImplementedException();
		}

		public void LaunchTimer(long userId, long taskId)
		{
			throw new NotImplementedException();
		}

		public TimeSpan StopTimer(long userId, long taskId)
		{
			throw new NotImplementedException();
		}
	}
}
