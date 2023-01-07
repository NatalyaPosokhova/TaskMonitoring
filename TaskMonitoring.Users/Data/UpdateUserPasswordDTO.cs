using System.ComponentModel.DataAnnotations;

namespace TaskMonitoring.Users.Data
{
	public struct UpdateUserPasswordDTO
	{
		[Required]
		public long Id { get; set; }
		[Required]
		public string NewPassword { get; set; }
	}
}
