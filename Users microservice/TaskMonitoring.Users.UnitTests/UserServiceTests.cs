using NUnit.Framework;
using TaskMonitoring.Users.BL;
using TaskMonitoring.Users.BL.Interface;
using NSubstitute;
using TaskMonitoring.Users.DataAccess.Interface;
using TaskMonitoring.Users.DataAccess.Interface.Models;
using TaskMonitoring.Users.BL.Exceptions;
using TaskMonitoring.Users.BL.Interface.DTO;
using TaskMonitoring.Utilities;

namespace TaskMonitoring.Users.UnitTests
{
	public class UserServiceTests
	{
		private IUserService _userService;
		private IDataAccess _dataAccess;

		[SetUp]
		public void Setup()
		{
			_dataAccess = Substitute.For<IDataAccess>();
			_userService = new UserService(_dataAccess);
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
				Login = login,
				Password = password
			};

			//act
			_dataAccess.CreateUser(expectedUser).Returns(expUserId);
			var actUserId = _userService.CreateUser(login, password).Id;

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
		public void GetUserByIdShouldBeSuccess()
		{
			//arrange
			long userId = 123;
			string login = "login";
			string password = "password";
			var expUser = new User
			{
				Id = userId,
				Login = login,
				Password = password
			};
			_dataAccess.GetUserById(userId).Returns(expUser);

			//act
			var actUser = _userService.GetUserById(userId);

			//assert
			Assert.AreEqual(Util<User, UserDTO>.Map(expUser), actUser);
		}

		[Test]
		public void UpdateUserShouldBeSuccess()
		{
			//arrange
			long userId = 123;
			string login = "login";
			string newPassword = "newPassword";
			string oldPassword = "password";
			var oldUser = new User
			{
				Id = userId,
				Login = login,
				Password = oldPassword
			};
			var newUser = new User
			{
				Id = userId,
				Login = login,
				Password = newPassword
			};

			//act
			_dataAccess.GetUserById(userId).Returns(oldUser);
			_userService.UpdateUserPassword(userId, newPassword);

			//assert
			_dataAccess.Received().UpdateUser(newUser);
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
		public void DeleteUserShouldBeSuccess()
		{
			//arrange
			long userId = 123;
			//act
			//assert

			_userService.DeleteUser(userId);
			_dataAccess.Received().DeleteUser(userId);
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
	}
}
