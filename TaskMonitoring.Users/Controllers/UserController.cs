using Microsoft.AspNetCore.Mvc;
using TaskMonitoring.Users.BL.Interface;
using TaskMonitoring.Users.BL.Interface.DTO;
using TaskMonitoring.Users.Data;
using TaskMonitoring.Utilities;

namespace TaskMonitoring.Users.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserController : Controller
	{
		private readonly IUserService _userService;
		public UserController(IUserService userService)
		{
			_userService = userService;
		}
		
		[HttpPost]
		public IActionResult CreateUser(string login, string password)
		{
			try
			{
				var userDto = _userService.CreateUser(login, password);
				var user = Util<UserDTO, User>.Map(userDto);

				return new ObjectResult(user);
			}
			catch(Exception)
			{
				return new StatusCodeResult(500);
			}
		}

		[HttpPost]
		public IActionResult UpdateUserPassword(long id, string newPassword)
		{
			try
			{
				_userService.UpdateUserPassword(id, newPassword);
				return new OkResult();
			}
			catch(Exception)
			{
				return new StatusCodeResult(500);
			}
		}

		[HttpPost]
		public IActionResult DeleteUser(long id)
		{
			try
			{
				_userService.DeleteUser(id);
				return new OkResult();
			}
			catch(Exception)
			{
				return new StatusCodeResult(500);
			}
		}
	}
}
