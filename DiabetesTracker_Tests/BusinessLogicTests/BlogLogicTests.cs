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
        private Post _testPost;
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

            _testPost = new Post()
            {
                UserId = _testUser.UserId,
                BlogId = _testBlog.BlogId,
                Content = "TestPostContent",
                Image = new byte[] { 2, 4, 8, 16 },
                LikeCount = 1,
                CommentCount = 1
            };
            _dBContext.Posts.Add(_testPost);
            _dBContext.SaveChanges();
        }
        [TearDown]
        public void TearDown()
        {
            _dBContext.Posts.Remove(_testPost);
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

            Assert.IsNull(BlogLogic.DbContext);
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

        [Test]
        public void Test_BlogLogic_GetCurrentUserBlogs()
        {
            List<Blog> blogs = BlogLogic.GetCurrentUserBlogs(_testUser.UserId);

            Assert.That(blogs.Count == 1);
        }

        [Test]
        public void Test_BlogLogic_GetBlogPostsCount()
        {
            int postCount = BlogLogic.GetBlogPostsCount(_testBlog.BlogId);

            Assert.That(postCount == 1);
        }

        [Test]
        public void Test_BlogLogic_GetBlogFollowingCount()
        {
            int followingCount = BlogLogic.GetBlogFollowingCount(_testUser.UserId);

            Assert.That(followingCount == 1);
        }

        [Test]
        public void Test_BlogLogic_GetAllPosts()
        {
            List<Post> posts = BlogLogic.GetAllPosts(_testBlog.BlogId);

            Assert.That(posts.Count == 1);
        }

        [Test]
        public void Test_BlogLogic_GetLeastFollowedBlod()
        {
            string blogName = BlogLogic.GetLeastFollowedBlod(_testUser.UserId);

            Assert.That(blogName == _testBlog.Name);
        }

        [Test]
        public void Test_FollowingBlogLogic_Follow_Unfollow()
        {
            FollowingBlogLogic.Follow(_testUser.UserId, _testBlog.BlogId);
            _dBContext.SaveChanges();

            FollowingBlog followingBlog = _dBContext.FollowingBlogs.Where(followingBlog => followingBlog.BlogId == _testBlog.BlogId && followingBlog.UserId == _testUser.UserId).First();
            FollowingBlogLogic.Unfollow(_testUser.UserId, _testBlog.BlogId);

            Assert.IsNotNull(followingBlog);
        }

        [Test]
        public void Test_FollowingBlogLogic_IsCurrentUserFollowed_Followed()
        {
            FollowingBlogLogic.Follow(_testUser.UserId, _testBlog.BlogId);
            _dBContext.SaveChanges();

            bool result = FollowingBlogLogic.IsCurrentUserFollowed(_testUser.UserId, _testBlog.BlogId);
            FollowingBlogLogic.Unfollow(_testUser.UserId, _testBlog.BlogId);

            Assert.That(result);

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