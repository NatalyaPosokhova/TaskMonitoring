using System.Linq;

namespace TaskMonitoring.Users.DataAccess.Interface.Models
{
	public class User
	{
		public long Id { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }

		public override bool Equals(object obj)
		{
			if(obj == null)
				return false;
			if(obj is not User )
				return false;

			var user = (User)obj;
			return user.Id == Id && user.Login == Login && user.Password == Password;
		}
	}
}
