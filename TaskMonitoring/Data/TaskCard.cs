using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskMonitoring.Cards.Data
{
	public class TaskCard
	{
		public long Id { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Summary { get; set; }
		public IList<string> Comments { get; set; }
	}
}
