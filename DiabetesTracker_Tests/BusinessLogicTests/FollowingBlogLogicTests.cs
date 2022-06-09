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
    public class FollowingBlogLogicTests
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
        public void Test_FollowingBlogLogic_Follow_Unfollow()
        {
            FollowingBlogLogic.Follow(_testUser.UserId, _testBlog.BlogId);
            _dBContext.SaveChanges();

            FollowingBlog followingBlog = _dBContext.FollowingBlogs.Where(followingBlog => followingBlog.BlogId == _testBlog.BlogId && followingBlog.UserId == _testUser.UserId).First();

            Assert.IsNotNull(followingBlog);

            FollowingBlogLogic.Unfollow(_testUser.UserId, _testBlog.BlogId);
        }

        [Test]
        public void Test_FollowingBlogLogic_IsCurrentUserFollowed_Followed()
        {
            FollowingBlogLogic.Follow(_testUser.UserId, _testBlog.BlogId);
            _dBContext.SaveChanges();

            Assert.That(FollowingBlogLogic.IsCurrentUserFollowed(_testUser.UserId, _testBlog.BlogId));

            FollowingBlogLogic.Unfollow(_testUser.UserId, _testBlog.BlogId);
        }

        [Test]
        public void Test_FavouritePostLogic_IsCurrentUserFavourited_NotFavourited()
        {
            Assert.That(FollowingBlogLogic.IsCurrentUserFollowed(_testUser.UserId, _testBlog.BlogId) == false);
        }

        [Test]
        public void Test_FavouritePostLogic_GetFavouritePosts_NoFavouritedPosts()
        {
            Assert.That(FollowingBlogLogic.GetFollowingBlogs(_testUser.UserId).Count == 0);
        }
    }
}
