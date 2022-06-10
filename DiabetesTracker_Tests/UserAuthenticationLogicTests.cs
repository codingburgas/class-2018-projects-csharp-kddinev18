using BusinessLogicLayer;
using DataAccessLayer;
using DataAccessLayer.Data.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesTracker_Tests
{
    public class UserAuthenticationLogicTests
    {
        private DiabetesTrackerDbContext _dBContext;
        private User _testUser;
        [SetUp]
        public void Setup()
        {
            _dBContext = Master.OpenConnection();
            _testUser = new User()
            {
                UserName = "TestUser",
                Email = "Tu@gamil.com",
                Password = "AHASHEDPASSWORD",
                Salt = "123",
                AccountStatus = true,
                IsAdmin = false
            };
            _dBContext.Users.Add(_testUser);
            _dBContext.SaveChanges();
        }
        [TearDown]
        public void TearDown()
        {
            _dBContext.Users.Remove(_testUser);
            _dBContext.SaveChanges();
            Master.CloseConnection();
        }

        [Test]
        public void Test_UserLogic_Register()
        {
            int userId = UserLogic.Register("TestUser1", "Testemail@email", "Testpassword1!");
            _dBContext.Users.Remove(_dBContext.Users.Where(user => user.UserId == userId).First());
            _dBContext.SaveChanges();

            Assert.That(userId != 0);
        }

        [Test]
        public void Test_UserLogic_Register_WithSameUserName()
        {
            Assert.Throws<ArgumentException>(
                () =>
                {
                    UserLogic.Register("TestUser", "Testemail@email", "Testpassword1!");
                });
        }

        [TestCase("123")]
        [TestCase("asdasdaasdasd")]
        [TestCase("asdasdadasdasd123")]
        [TestCase("asd!!2s")]
        [TestCase("TTshv25")]
        public void Test_UserLogic_Register_WithIncorentPassword(string password)
        {
            Assert.Throws<ArgumentException>(
                () =>
                {
                    UserLogic.Register("TestUser", "Testemail@email", password);
                });
        }

        [Test]
        public void Test_UserLogic_CheckUserProfile()
        {
            Assert.That(UserLogic.CheckUserProfile(_testUser.UserId) == false);
        }

        [Test]
        public void Test_UserLogic_CheckUserProfile_WithUserProfile()
        {
            UserProfile userProfile = new UserProfile()
            {
                UserId = _testUser.UserId,
                Gender = 'M',
                About = "About",
                City = "city",
                Country = "Country"
            };

            _dBContext.UserProfiles.Add(userProfile);
            _dBContext.SaveChanges();

            bool result = UserLogic.CheckUserProfile(_testUser.UserId);

            _dBContext.UserProfiles.Remove(userProfile);
            _dBContext.SaveChanges();

            Assert.That(result);
        }

        [Test]
        public void Test_UserLogic_LogIn_WrongCredentials()
        {
            Assert.Throws<ArgumentException>(
                () =>
                {
                    UserCredentials userCredentials = UserLogic.LogIn("not a real username", "not a real password");
                });
        }

        [TestCase(' ',"about","Burgas","Bulgaria")]
        [TestCase('M',"","Burgas","Bulgaria")]
        [TestCase('M',"about", "", "Bulgaria")]
        [TestCase('M',"about","Burgas", "")]
        [TestCase("", "", "", "")]
        public void Test_UserLogic_ConfigureUserProfile_NullInput(char gender, string about, string country, string city)
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    UserProfileLogic.ConfigureUserProfile(_testUser.UserId,gender, about, country, city);
                });
        }
    }
}
