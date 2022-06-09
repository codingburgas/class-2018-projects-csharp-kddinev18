using BusinessLogicLayer;
using DataAccessLayer;
using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace DiabetesTracker_Tests
{
    public class BusinessLogicTest
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
            _dBContext.Remove(_testUser);
            Master.CloseConnection();
        }

        [Test]
        public void MasterOpenConnectionTest()
        {
            Master.OpenConnection();

            Assert.That(BlogLogic.DbContext is not null);
        }

        [Test]
        public void MasterCloseConnectionTest()
        {
            Master.OpenConnection();
            Master.CloseConnection();

            Assert.That(BlogLogic.DbContext is null);
        }
    }
}