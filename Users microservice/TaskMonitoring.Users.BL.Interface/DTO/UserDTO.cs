using System.Collections.Generic;

namespace TaskMonitoring.Users.BL.Interface.DTO
{
	public class UserDTO
	{
		public long Id { get; set; }
		public string Login {get; set;}
		public string Password { get; set; }
	}
}
