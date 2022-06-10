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
    public class PostLogicTests
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
        public void Test_PostCommentLogic_Comment_GetComments()
        {
            PostCommentLogic.Comment(_testUser.UserId, _testPost.PostId, "TestComment");

            string comment = PostCommentLogic.GetComments(_testUser.UserId, _testPost.PostId).First();

            Assert.That(comment == "TestComment");
        }
    }
}
