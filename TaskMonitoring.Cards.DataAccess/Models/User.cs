using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMonitoring.Cards.DataAccess.Models
{
	public class User
	{
		public long Id { get; set; }
		public IEnumerable<Comment> Tasks { get; set; }
	}
}
