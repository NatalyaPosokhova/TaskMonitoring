using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMonitoring.APIClients.Tasks.DTO
{
	public class TaskProxyDTO
	{
		public long Id { get; set; }
		public string Title { get; set; }
		public string Summary { get; set; }
		public long UserId { get; set; }

		public IList<string> Comments { get; set; }
	}
}
