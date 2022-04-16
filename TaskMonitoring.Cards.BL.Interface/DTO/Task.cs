using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMonitoring.Cards.BL.Interface.DTO
{
	public class Task
	{
		public string Title { get; set; }
		public string Summary { get; set; }
		public int Id { get; set; }

		public IEnumerable<string> Comments { get; set; }
	}
}
