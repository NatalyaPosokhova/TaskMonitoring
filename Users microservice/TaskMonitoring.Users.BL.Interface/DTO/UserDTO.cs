using System.Collections.Generic;

namespace TaskMonitoring.Users.BL.Interface.DTO
{
	public class UserDTO
	{
		public long Id { get; set; }
		public string Login {get; set;}
		public string Password { get; set; }

		public override bool Equals(object obj)
		{
			if(obj == null)
				return false;

			var user = obj as UserDTO;
			if(user == null)
				return false;

			return user.Id == Id && user.Login == Login && user.Password == Password;
		}
	}
}
