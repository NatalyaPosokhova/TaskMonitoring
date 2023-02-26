using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMonitoring.APIClients.Tasks;
using TaskMonitoring.Timing.BL.Interface;
using TaskMonitoring.Timing.BL.Interface.Exceptions;
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

		public void LaunchTimer(long userId, long taskId, DateTime startTime)
		{
			try
			{
				var task = _tasksProxy.GetTaskById(userId, taskId).Result;
				if(task == null)
					throw new TaskNotFoundException($"Задачи Id: {taskId} или пользователя Id: {userId} не существует.");

				_dataAccess.PutStartTaskTime(userId, taskId, startTime);
			}
			catch(Exception ex)
			{
				throw new CannotLaunchTimerForTaskException("Не удалось запустить таймер", ex);
			}
		}

		public TimeSpan StopTimer(long userId, long taskId)
		{
			try
			{
				var task = _tasksProxy.GetTaskById(userId, taskId).Result;
				if(task == null)
					throw new TaskNotFoundException($"Задачи Id: {taskId} или пользователя Id: {userId} не существует.");

				var startTime = _dataAccess.GetStartTimeForTask(userId, taskId);
				if(startTime == null)
					throw new TryStopNotLaunchedTimerException($"Задача с Id {taskId} для пользователя Id {userId} не запущена.");

				var stopTime = DateTime.Now;
				_dataAccess.PutStopTaskTime(userId, taskId, stopTime);

				var result =  stopTime - (DateTime)startTime;
				return result;
			}
			catch(Exception ex)
			{
				throw new CannotStopTimerForTaskException("Не удалось остановить таймер", ex);
			}
		}
	}
}
