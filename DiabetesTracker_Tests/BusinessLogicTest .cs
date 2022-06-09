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
        public void Test_Master_OpenConnection()
        {
            Master.OpenConnection();

            Assert.That(BlogLogic.DbContext is not null);
        }

        [Test]
        public void Test_Master_CloseConnection()
        {
            Master.OpenConnection();
            Master.CloseConnection();

            Assert.That(BlogLogic.DbContext is null);
        }


        [TestCase("TestBlogName", new byte[] { 2, 4, 8, 16 })]
        public void Test_BlogLogic_CreateBlog(string name, byte[] image)
        {
            Blog testBlog = BlogLogic.CreateBlog(_testUser.UserId, name, image);
            Blog blogFromDatabase = _dBContext.Blogs.Where(blog => blog.BlogId == testBlog.BlogId).First();

            Assert.That(testBlog.BlogId == blogFromDatabase.BlogId && testBlog.Name == blogFromDatabase.Name);

            _dBContext.Remove(blogFromDatabase);
        }

        [TestCase("TestBlogName", new byte[] { 2, 4, 8, 16 })]
        public void Test_BlogLogic_BelongsToUser(string name, byte[] image)
        {
            Blog testBlog = BlogLogic.CreateBlog(_testUser.UserId, name, image);

            Assert.That(BlogLogic.BelogsToUser(_testUser.UserId, testBlog.BlogId) == true);

            _dBContext.Remove(testBlog);
        }
    }
}