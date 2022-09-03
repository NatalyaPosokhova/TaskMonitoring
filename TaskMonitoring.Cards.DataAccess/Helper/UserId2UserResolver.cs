using AutoMapper;
using System.Linq;
using TaskMonitoring.Cards.DataAccess.Interface;
using TaskMonitoring.Cards.DataAccess.Models;


namespace TaskMonitoring.Cards.DataAccess.Helper
{
	public class UserId2UserResolver : IValueResolver<TaskDataAccessDTO, Task, User>
	{
		public User Resolve(TaskDataAccessDTO source, Task destination, User destMember, ResolutionContext context)
		{
			return new User
			{ 
				Id = source.UserId, 
				Tasks = source.Comments.Select(c => new Comment { Content = c, TaskId = source.Id}).ToList()
			};
		}
	}
}
