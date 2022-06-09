using BusinessLogicLayer;
using DataAccessLayer;
using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiabetesTracker_Tests
{
    public class BlogLogicTests
    {
        private DiabetesTrackerDbContext _dBContext;
        private User _testUser;
        private Blog _testBlog;
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

            _testBlog = new Blog()
            {
                UserId = _testUser.UserId,
                Name = "TestBlogName",
                Image = new byte[] { 2, 4, 8, 16 },
                FollowingCount = 1,
            };
            _dBContext.Blogs.Add(_testBlog);
            _dBContext.SaveChanges();
        }
        [TearDown]
        public void TearDown()
        {
            _dBContext.Blogs.Remove(_testBlog);
            _dBContext.Users.Remove(_testUser);
            _dBContext.SaveChanges();
            Master.CloseConnection();
        }

        [Test]
        public void Test_Master_OpenConnection()
        {
            Assert.That(_dBContext is not null);
        }

        [Test]
        public void Test_Master_CloseConnection()
        {
            Master.OpenConnection();
            Master.CloseConnection();

            Assert.That(BlogLogic.DbContext is null);
        }


        [Test]
        public void Test_BlogLogic_CreateBlog()
        {
            Blog blog = _dBContext.Blogs.Where(blog => blog.BlogId == _testBlog.BlogId).First();

            Assert.That(_testBlog.BlogId == blog.BlogId && _testBlog.Name == blog.Name);
        }

        [Test]
        public void Test_BlogLogic_BelongsToUser()
        {
            Assert.That(BlogLogic.BelogsToUser(_testUser.UserId, _testBlog.BlogId) == true);
        }

        [TestCase("TestBlogName")]
        public void Test_BlogLogic_GetBlogsByName(string name)
        {
            List<Blog> blogs = BlogLogic.GetBlogsByName(name);

            Assert.That(blogs.Count >= 1);
        }

    }
}