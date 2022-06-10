using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMonitoring.Cards.DataAccess.Models
{
	public class Comment
	{
		public long Id { get; set; }
		public string Content { get; set; }
		public long TaskId { get; set; }
	}
}
