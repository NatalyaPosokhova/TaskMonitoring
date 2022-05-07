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

		public override bool Equals(object obj)
		{
			//Check for null and compare run-time types.
			if ((obj == null) || !this.GetType().Equals(obj.GetType()))
			{
				return false;
			}
			else
			{
				Task task = (Task)obj;
				return (Id == task.Id) && (Title == task.Title) && (Summary == task.Summary);
			}
		}
	}
}
