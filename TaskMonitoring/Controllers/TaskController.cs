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
	[Route("api/[controller]")]
	[ApiController]
	public class TaskController : ControllerBase
	{
		private readonly ITaskService _taskService;
		public TaskController(ITaskService taskService)
		{
			_taskService = taskService;
		}

		[HttpPost]
		public async Task<IActionResult> CreateTask(long userId, NewTaskCard task)
		{
			try
			{ 
				if(!ModelState.IsValid)
				{
					return new StatusCodeResult(422);
				}
				var mappedTask = Util<NewTaskCard, TaskDTO>.Map(task);
				var createdTask = await _taskService.CreateTask(userId, mappedTask);

				return new ObjectResult(createdTask);
			}
			catch(CannotCreateTaskException)
			{
				return new StatusCodeResult(500);
			}
		}
	}
}
