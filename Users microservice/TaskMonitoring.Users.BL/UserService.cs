using System;
using TaskMonitoring.Users.BL.Exceptions;
using TaskMonitoring.Users.BL.Interface;
using TaskMonitoring.Users.BL.Interface.DTO;
using TaskMonitoring.Users.DataAccess.Interface;
using TaskMonitoring.Users.DataAccess.Interface.Models;
using TaskMonitoring.Utilities;

namespace TaskMonitoring.Users.BL
{
	public class UserService : IUserService
	{

		public IDataAccess _dataAccess;
		public UserService(IDataAccess dataAccess)
		{
			_dataAccess = dataAccess;
		}

		public UserDTO CreateUser(string login, string password)
		{

			if(string.IsNullOrEmpty(login))
				throw new EmptyLoginException("");
			if(password == null || password.Length < 5)
				throw new PasswordLengthException("");

			var user = new User
			{
				Login = login,
				Password = password
			};

			try
			{
				var userId = _dataAccess.CreateUser(user);
				user.Id = userId;
				return Util<User, UserDTO>.Map(user);
			}
			catch(DataAccess.Interface.Exceptions.UserAlreadyExistsException) 
			{
				throw new UserAlreadyExistsException("");
			}
			catch(Exception ex)
			{
				throw new CannotCreateUserException("");
			}

		}

		public void DeleteUser(long id)
		{
			try
			{
				_dataAccess.DeleteUser(id);
			}
			catch(DataAccess.Interface.Exceptions.UserNotFoundException ex)
			{
				throw new UserNotFoundException("");
			}
			catch(Exception ex)
			{
				throw new CannotDeleteUserException("");
			}
		}

		public UserDTO GetUserById(long id)
		{
			try
			{
				var user = _dataAccess.GetUserById(id);
				return Util<User, UserDTO>.Map(user);
			}
			catch(DataAccess.Interface.Exceptions.UserNotFoundException ex)
			{
				throw new UserNotFoundException("");
			}
			catch(Exception ex)
			{
				throw new CannotGetUserException("");
			}
		}

		public void UpdateUserPassword(long id, string newPassword)
		{
			try
			{
				if(newPassword == null || newPassword.Length < 5)
					throw new PasswordLengthException("");

				var user = _dataAccess.GetUserById(id);
				if(user.Password != newPassword)
				{
					user.Password = newPassword;
					_dataAccess.UpdateUser(user);
				}

			}
			catch(DataAccess.Interface.Exceptions.UserNotFoundException ex)
			{
				throw new UserNotFoundException("");
			}
			catch(Exception)
			{
				throw new CannotUpdateUserException("");
			}
		}
	}
}
