using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using TaskMonitoring.Cards.DataAccess.Interface;
using TaskMonitoring.Cards.DataAccess.Models;

namespace TaskMonitoring.Cards.DataAccess.Helper
{
	public class Comment2StringResolver : IValueResolver<Task, TaskDataAccessDTO, IList<string>>
	{
		public IList<string> Resolve(Task source, TaskDataAccessDTO destination, IList<string> destMember, ResolutionContext context)
		{
			return source.Comments.Select(c => c.Content).ToList();
		}
	}
}
