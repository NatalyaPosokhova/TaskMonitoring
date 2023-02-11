using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TaskMonitoring.Users.BL.Interface;
using TaskMonitoring.Users.BL.Interface.DTO;
using TaskMonitoring.Users.Data;
using TaskMonitoring.Users.DataAccess.Interface.Exceptions;
using TaskMonitoring.Utilities;

namespace TaskMonitoring.Users.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class UserController : Controller
	{
		private readonly IUserService _userService;
		public UserController(IUserService userService)
		{
			_userService = userService;
		}
		
		[HttpPost]
		public IActionResult CreateUser(CreateUserDTO createUserDto)
		{
			try
			{
				var userDto = _userService.CreateUser(createUserDto.Login, createUserDto.Password);
				var user = Util<UserDTO, User>.Map(userDto);

				return new ObjectResult(user);
			}
			catch(UserAlreadyExistsException ex)
			{
				return new BadRequestObjectResult(new { err = ex.Message, innerException = ex.InnerException });
			}
			catch(Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost]
		public IActionResult UpdateUserPassword(UpdateUserPasswordDTO updateUserPasswordDto)
		{
			try
			{
				_userService.UpdateUserPassword(updateUserPasswordDto.Id, updateUserPasswordDto.NewPassword);
				return new OkResult();
			}
			catch(Exception)
			{
				return new StatusCodeResult(500);
			}
		}

		[HttpPost]
		public IActionResult DeleteUser(DeleteUserDTO deleteUserDTO)
		{
			try
			{
				_userService.DeleteUser(deleteUserDTO.Id);
				return new OkResult();
			}
			catch(Exception)
			{
				return new StatusCodeResult(500);
			}
		}

		[HttpGet]
		public IActionResult GetUserById(long userId)
		{
			try
			{
				var userDTO = _userService.GetUserById(userId);
				var user = Util<UserDTO, User>.Map(userDTO);
				return new ObjectResult(user);
			}
			catch(Exception ex)
			{
				return StatusCode(500, ex);
			}
		}
	}
}
