using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMonitoring.Cards.DataAccess.Models
{
	public class Task
	{
		public long Id { get; set; }
		public string Title { get; set; }
		public string Summary { get; set; }
		public User User { get; set; }
		public IEnumerable<Comment> Comments { get; set; }
	}
}
