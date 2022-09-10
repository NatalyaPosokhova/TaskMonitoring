using NUnit.Framework;
using TaskMonitoring.Users.BL;
using TaskMonitoring.Users.BL.Interface;
using NSubstitute;
using TaskMonitoring.Users.DataAccess.Interface;
using TaskMonitoring.Users.DataAccess.Interface.Models;
using TaskMonitoring.Users.BL.Exceptions;
using System.Collections.Generic;

namespace TaskMonitoring.Users.UnitTests
{
	public class UserServiceTests
	{
		private IUserService _userService;
		private IDataAccess _dataAccess;

		[SetUp]
		public void Setup()
		{
			_userService = new UserService();
			_dataAccess = Substitute.For<IDataAccess>();
		}

		[TearDown]
		public void ClearUserTasks()
		{

		}

		[Test]
		public void CreateNewUserShouldBeSuccess()
		{
			//arrange
			string login = "login";
			string password = "password";
			long expUserId = 123;
			var expectedUser = new User 
			{
				Id = expUserId,
				Login = login,
				Password = password
			};

			//act
			var actUserId = _userService.CreateUser(login, password);
			_dataAccess.Received().CreateUser(expectedUser);

			//assert
			Assert.AreEqual(expUserId, actUserId);
		}

		[Test]
		public void TryCreateUserWithEmptyLoginShouldBeException()
		{
			//arrange
			string login = "";
			string password = "password";

			//act
			//assert
			Assert.Throws<EmptyLoginException>(() => _userService.CreateUser(login, password));
		}

		[Test]
		public void TryCreateUserWithAlreadyExistedLoginShouldBeException()
		{
			//arrange
			string login = "login";
			string password = "password";
			long expUserId = 123;
			var user = new User
			{
				Id = expUserId,
				Login = login,
				Password = password
			};
			_dataAccess.When(x => x.CreateUser(user)).Do(x => { throw new DataAccess.Interface.Exceptions.UserAlreadyExistsException(""); });

			//act
			//assert
			Assert.Throws<UserAlreadyExistsException>(() => _userService.CreateUser(login, password));
		}

		[Test]
		public void TryCreateUserWithPasswordLessFiveSymbolsShouldBeException()
		{
			//arrange
			string login = "login";
			string password = "pass";

			//act
			//assert
			Assert.Throws<PasswordLengthException>(() => _userService.CreateUser(login, password));
		}

		[Test]
		public void GetNotExistedUserByIdShouldBeException()
		{
			//arrange
			long userId = 123;
			_dataAccess.When(x => x.GetUserById(userId)).Do(x => { throw new DataAccess.Interface.Exceptions.UserNotFoundException(""); });
			//act			
			//assert
			Assert.Throws<UserNotFoundException>(() => _userService.GetUserById(userId));
		}

		[Test]
		public void UpdateNotExistedUserPasswordShouldBeException()
		{
			//arrange
			string login = "login";
			string password = "password";
			long userId = 123;
			var user = new User
			{
				Id = userId,
				Login = login,
				Password = password
			};

			_dataAccess.When(x => x.GetUserById(userId)).Do(x => { throw new DataAccess.Interface.Exceptions.UserNotFoundException(""); });

			//act
			//assert

			Assert.Throws<UserNotFoundException>(() => _userService.UpdateUserPassword(userId, password));

		}

		[Test]
		public void TryDeleteNotExistedUserShouldBeException()
		{
			//arrange
			long userId = 123;
			_dataAccess.When(x => x.DeleteUser(userId)).Do(x => { throw new DataAccess.Interface.Exceptions.UserNotFoundException(""); });
			//act
			//assert
			Assert.Throws<UserNotFoundException>(() => _userService.DeleteUser(userId));

		}

		[Test]
		public void TryGetAllUserTasksShouldBeSuccess()
		{
			//arrange
			long userId = 123;

			var expTasksIds = new List<long> { 111, 222, 333 };

			//act
			_dataAccess.GetAllUserTasks(userId).Returns(expTasksIds);
			var actTasksIds = _userService.GetAllUserTasks(userId);

			//act
			//assert
			Assert.AreEqual(expTasksIds, actTasksIds);

		}

	}
}
