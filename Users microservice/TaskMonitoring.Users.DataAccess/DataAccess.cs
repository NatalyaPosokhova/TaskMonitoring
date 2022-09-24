using System;
using System.Collections.Generic;
using TaskMonitoring.Users.DataAccess.Interface;
using TaskMonitoring.Users.DataAccess.Interface.Models;
using TaskMonitoring.Utilities;

namespace TaskMonitoring.Users.DataAccess
{
	public class DataAccess : IDataAccess, IDisposable
	{
		private readonly UserDbContext _db;
		public DataAccess(ContextFactory<UserDbContext> contextFactory)
		{
			_db = contextFactory.CreateDbContext(null);
		}
		public long CreateUser(User user)
		{
			throw new System.NotImplementedException();
		}

		public void DeleteUser(long id)
		{
			throw new System.NotImplementedException();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<long> GetAllUserTasks(long id)
		{
			throw new System.NotImplementedException();
		}

		public User GetUserById(long id)
		{
			throw new System.NotImplementedException();
		}

		public void UpdateUser(User user)
		{
			throw new System.NotImplementedException();
		}
	}
}
