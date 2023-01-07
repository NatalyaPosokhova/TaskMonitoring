using System.ComponentModel.DataAnnotations;

namespace TaskMonitoring.Users.Data
{
	public class DeleteUserDTO
	{
		[Required]
		public long Id { get; set; }
	}
}
