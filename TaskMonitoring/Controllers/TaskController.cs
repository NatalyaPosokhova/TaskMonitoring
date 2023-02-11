using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskMonitoring.Cards.BL.Exceptions;
using TaskMonitoring.Cards.BL.Interface;
using TaskMonitoring.Cards.BL.Interface.DTO;
using TaskMonitoring.Cards.Data;
using TaskMonitoring.Utilities;

namespace TaskMonitoring.Cards.Controllers
{
	[Route("[controller]/[action]")]
	[ApiController]
	public class TaskController : ControllerBase
	{
		private readonly ITaskService _taskService;
		public TaskController(ITaskService taskService)
		{
			_taskService = taskService;
		}

		[HttpPost]
		public IActionResult CreateTask(long userId, NewTaskCard task)
		{
			try
			{ 
				if(!ModelState.IsValid)
				{
					return new StatusCodeResult(422);
				}
				var mappedTask = Util<NewTaskCard, TaskDTO>.Map(task);
				var createdTask = _taskService.CreateTask(userId, mappedTask);

				return new ObjectResult(createdTask);
			}
			catch(CannotCreateTaskException)
			{
				return new StatusCodeResult(500);
			}
		}

		[HttpPost]
		public IActionResult DeleteTaskById(long userId, long taskId)
		{
			try
			{
				if(!ModelState.IsValid)
				{
					return new StatusCodeResult(422);
				}

				_taskService.DeleteTaskById(userId, taskId);

				return new OkResult();
			}
			catch(CannotDeleteTaskException)
			{
				return new StatusCodeResult(500);
			}
		}

		[HttpPost]
		public IActionResult AddComment(long userId, long taskId, string comment)
		{
			try
			{
				if(!ModelState.IsValid)
				{
					return new StatusCodeResult(422);
				}

				_taskService.AddComment(userId, taskId, comment);

				return new OkResult();
			}
			catch(CannotAddCommentException)
			{
				return new StatusCodeResult(500);
			}
		}

		[HttpGet]
		public IActionResult GetAllTasks(long userId)
		{
			try
			{
				if(!ModelState.IsValid)
				{
					return new StatusCodeResult(422);
				}
				var tasks = _taskService.GetAllTasks(userId);

				return new ObjectResult(tasks);
			}
			catch(CannotGetAllTasksException)
			{
				return new StatusCodeResult(500);
			}
		}

		[HttpPut]
		public IActionResult UpdateTask(long userId, NewTaskCard task)
		{
			try
			{
				if(!ModelState.IsValid)
				{
					return new StatusCodeResult(422);
				}
				var mappedTask = Util<NewTaskCard, TaskDTO>.Map(task);
				_taskService.UpdateTask(userId, mappedTask);

				return new OkResult();
			}
			catch(CannotUpdateTaskException)
			{
				return new StatusCodeResult(500);
			}
		}

		[HttpGet]
		public IActionResult GetTaskById(long userId, long taskId)
		{
			try
			{
				if(!ModelState.IsValid)
				{
					return new StatusCodeResult(422);
				}

				var task = _taskService.GetTaskById(userId, taskId);
				return new ObjectResult(task);
			}
			catch(CannotGetTaskByIdException)
			{
				return new StatusCodeResult(500);
			}
		}
	}
}
