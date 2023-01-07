using System.ComponentModel.DataAnnotations;

namespace TaskMonitoring.Users.Data
{
	public struct CreateUserDTO
	{
		[Required]
		public string Login { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
