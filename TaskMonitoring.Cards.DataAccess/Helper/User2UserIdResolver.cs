using AutoMapper;
using TaskMonitoring.Cards.DataAccess.Interface;
using TaskMonitoring.Cards.DataAccess.Models;

namespace TaskMonitoring.Cards.DataAccess.Helper
{
	public class User2UserIdResolver : IValueResolver<Task, TaskDataAccessDTO, long>
	{
		public long Resolve(Task source, TaskDataAccessDTO destination, long destMember, ResolutionContext context)
		{
			return source.User.Id;
		}
	}
}
