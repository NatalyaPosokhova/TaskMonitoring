using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMonitoring.APIClients;
using TaskMonitoring.APIClients.Exceptions;
using TaskMonitoring.APIClients.Tasks;
using TaskMonitoring.APIClients.Tasks.DTO;
using TaskMonitoring.Timing.BL;
using TaskMonitoring.Timing.BL.Interface;
using TaskMonitoring.Timing.BL.Interface.Exceptions;
using TaskMonitoring.Timing.DataAccess.Interface;

namespace TaskMonitoring.Timing.UnitTests
{

	public class TimingServiceUnitTests
	{
		private ITimingService _timingService;
		private IDataAccess _dataAccess;
		private IWebAPITasks _tasksProxy;
		private const long USERID = 234;
		private const long TASKID = 345;
		private const long NOTEXISTEDTASKID = 765;
		private TaskProxyDTO _task;
		private DateTime STARTTIME = new DateTime(2023, 12, 12);

		[SetUp]
		public void SetUp()
		{
			_dataAccess = Substitute.For<IDataAccess>();
			_tasksProxy = Substitute.For<IWebAPITasks>();			
			_timingService = new TimingService(_dataAccess, _tasksProxy);

			_task = new TaskProxyDTO 
			{ 
				Id = TASKID, 
				Title = "titleTest",
				Summary = "summaryTest",
				Comments = new List<string> { "commentTest" },
				UserId = USERID
			};

		}

		[Test]
		public void LaunchTimerShouldBeSuccess()
		{
			//arrange
			_tasksProxy.GetTaskById(USERID, TASKID).Returns<TaskProxyDTO>(_task);

			//act
			_timingService.LaunchTimer(USERID, TASKID, STARTTIME);

			//assert
			_dataAccess.Received().PutStartTaskTime(USERID, TASKID, STARTTIME);
		}

		[Test]
		public void LaunchTimerForNotExistedTaskShouldBeFailed()
		{
			//arrange
			_tasksProxy.When(x => x.GetTaskById(USERID, NOTEXISTEDTASKID))
				.Do(x => { throw new CannotGetTaskByIdException(); });

			//act
			//assert
			Assert.Throws<CannotLaunchTimerForTaskException>(() => _timingService.LaunchTimer(USERID, NOTEXISTEDTASKID, STARTTIME));
		}

		[Test]
		public void LaunchTimerForNotExistedUserShouldBeFailed()
		{
			//arrange
			_tasksProxy.When(x => x.GetTaskById(USERID, NOTEXISTEDTASKID))
				.Do(x => { throw new UserNonExistedException(); });

			//act
			//assert
			Assert.Throws<CannotLaunchTimerForTaskException>(() => _timingService.LaunchTimer(USERID, NOTEXISTEDTASKID, STARTTIME));
		}

		[Test]
		public void LaunchTimerForAlreadyStartedTaskShouldBeSuccess()
		{
			//arrange
			var firstTime = new DateTime(2023, 12, 11);

			_tasksProxy.GetTaskById(USERID, TASKID).Returns<TaskProxyDTO>(_task);
			_timingService.LaunchTimer(USERID, TASKID, firstTime);
			//act
			//assert
			Assert.DoesNotThrow(() => _timingService.LaunchTimer(USERID, TASKID, STARTTIME));
			_dataAccess.Received().PutStartTaskTime(USERID, TASKID, STARTTIME);
		}

		[Test]
		public void StopTimerShouldBeSuccess()
		{
			//arrange
			_tasksProxy.GetTaskById(USERID, TASKID).Returns<TaskProxyDTO>(_task);
			_timingService.LaunchTimer(USERID, TASKID, STARTTIME);

			//act
			_timingService.StopTimer(USERID, TASKID);

			//assert
			_dataAccess.Received().PutStopTaskTime(USERID, TASKID, Arg.Any<DateTime>());
		}

		[Test]
		public void StopNotLaunchedTimerShouldBeException()
		{
			//arrange
			_tasksProxy.GetTaskById(USERID, TASKID).Returns<TaskProxyDTO>(_task);

			//act
			//assert
			Assert.Throws<TryStopNotLaunchedTimerException>(() => _timingService.StopTimer(USERID, TASKID));
		}

		[Test]
		public void StopTimerForNotExistedTaskShouldBeFailed()
		{
			//arrange
			_tasksProxy.When(x => x.GetTaskById(USERID, NOTEXISTEDTASKID))
				.Do(x => { throw new CannotGetTaskByIdException(); });

			//act
			//assert
			Assert.Throws<CannotStopTimerForTaskException>(() => _timingService.StopTimer(USERID, NOTEXISTEDTASKID));
		}

		[Test]
		public void StopTimerForNotExistedUserShouldBeFailed()
		{
			//arrange
			_tasksProxy.When(x => x.GetTaskById(USERID, NOTEXISTEDTASKID))
				.Do(x => { throw new UserNonExistedException(); });

			//act
			//assert
			Assert.Throws<CannotStopTimerForTaskException>(() => _timingService.StopTimer(USERID, NOTEXISTEDTASKID));
		}
	}
}
