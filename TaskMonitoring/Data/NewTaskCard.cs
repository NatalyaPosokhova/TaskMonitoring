using System.ComponentModel.DataAnnotations;

namespace TaskMonitoring.Cards.Data
{
	public class NewTaskCard
	{
		[Required]
		public string Title { get; set; }
		[Required]
		public string Summary { get; set; }
	}
}
