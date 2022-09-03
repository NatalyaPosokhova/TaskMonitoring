using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using TaskMonitoring.Cards.DataAccess.Interface;
using TaskMonitoring.Cards.DataAccess.Models;


namespace TaskMonitoring.Cards.DataAccess.Helper
{
	public class String2CommentResolver : IValueResolver<TaskDataAccessDTO, Task, IEnumerable<Comment>>
	{
		public IEnumerable<Comment> Resolve(TaskDataAccessDTO source, Task destination, IEnumerable<Comment> destMember, ResolutionContext context)
		{
			return source.Comments.Select(c => new Comment{ Content = c, TaskId = source.Id}).ToList();
		}
	}
}
