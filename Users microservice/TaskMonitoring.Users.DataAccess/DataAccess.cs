using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskMonitoring.Users.DataAccess.Interface;
using TaskMonitoring.Users.DataAccess.Interface.Exceptions;
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
			try
			{
				_db.Users.Add(user);
				_db.SaveChanges();

				return user.Id;
			}
			catch(DbUpdateException ex)
			{
				throw new UserAlreadyExistsException($"Пользователь с id: {user.Id} уже существует.", ex);
			}
			catch(Exception ex)
			{
				throw new CannotCreateUserException("Невозможно создать пользователя в базе данных.", ex);
			}
		}

		public void DeleteUser(long id)
		{
			try
			{
				_db.Database.ExecuteSqlInterpolated($"DELETE FROM public.\"Users\" WHERE \"Id\" = {id}");
				_db.SaveChanges();
			}
			catch(Exception ex)
			{
				throw new CannotDeleteUserException($"Невозможно удалить пользователя с id = {id}.", ex);
			}
		}

		public void Dispose()
		{
			_db.Dispose();
		}

		public User GetUserById(long id)
		{
			User user;
			try
			{
				user = _db.Users.Where(user => user.Id == id).FirstOrDefault();
			}
			catch(Exception ex)
			{
				throw new CannotGetUserException($"Не удалось получить пользователя из базы данных с id = {id}", ex);
			}

			if(user == null)
				throw new UserNotFoundException($"Пользователя c id = {id} не существует.");

			return user;
		}

		public void UpdateUser(User user)
		{
			try
			{
				_db.Database.ExecuteSqlInterpolated($"UPDATE Users SET Password = {user.Password} WHERE Id = {user.Id}");
				_db.SaveChanges();
			}
			catch(Exception)
			{
				throw new CannotUpdateUserException($"Невозможно обновить пароль у пользователя с id {user.Id}.");
			}
		}
	}
}
