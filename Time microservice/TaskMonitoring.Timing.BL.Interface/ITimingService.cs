using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMonitoring.Timing.BL.Interface
{
	public interface ITimingService
	{
		public void LaunchTimer(long userId, long taskId);
		public TimeSpan StopTimer(long userId, long taskId);
		public IDictionary<long, IEnumerable<TimeSpan>> GetReport(long userId, IEnumerable<long> tasks);
	}
}
