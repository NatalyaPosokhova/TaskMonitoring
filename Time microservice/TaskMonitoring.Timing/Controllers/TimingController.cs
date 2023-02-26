using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskMonitoring.Timing.BL.Interface;

namespace TaskMonitoring.Timing.Controllers
{
	[Route("[controller]/[action]")]
	[ApiController]
	public class TimingController : ControllerBase
	{
		private readonly ITimingService _timingService;
		public TimingController(ITimingService timingService)
		{
			_timingService = timingService;
		}

		[HttpPost]
		public void LaunchTimer(long userId, long taskId, DateTime startTime)
		{
			throw new NotImplementedException();
		}

		[HttpPost]
		public TimeSpan StopTimer(long userId, long taskId)
		{
			throw new NotImplementedException();
		}

		[HttpPost]
		public IDictionary<long, IEnumerable<TimeSpan>> GetReport(long userId, IEnumerable<long> tasks)
		{
			throw new NotImplementedException();
		}
	}
}
