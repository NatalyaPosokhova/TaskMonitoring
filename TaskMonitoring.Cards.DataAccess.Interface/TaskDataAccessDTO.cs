﻿using System;
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

		public override bool Equals(object obj)
		{
			//Check for null and compare run-time types.
			if ((obj == null) || !this.GetType().Equals(obj.GetType()))
			{
				return false;
			}
			else
			{
				TaskDataAccessDTO task = (TaskDataAccessDTO)obj;
				return (Id == task.Id) && (Title == task.Title) && (Comments == task.Comments) && (Summary == task.Summary);
			}
		}
	}
}
