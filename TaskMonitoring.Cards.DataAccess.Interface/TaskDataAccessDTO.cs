using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMonitoring.Cards.DataAccess.Interface
{
	public class TaskDataAccessDTO
	{
		public string Title { get; set; }
		public string Summary { get; set; }
		public long Id { get; set; }
		public IList<string> Comments { get; set; }
	}
}
