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
        public void Test_FavouritePostLogic_Favourite_Unfavourite()
        {
            FavouritePostLogic.Favourite(_testPost.PostId, _testUser.UserId);

            FavouritePost favouritePost = _dBContext.FavouritePosts.Where(favouritePost => favouritePost.PostId == _testPost.PostId && favouritePost.UserId == _testUser.UserId).First();

            Assert.IsNotNull(favouritePost);

            FavouritePostLogic.Unfavourite(_testPost.PostId, _testUser.UserId);

        }

        [Test]
        public void Test_FavouritePostLogic_IsCurrentUserFavourited_Favourited()
        {
            FavouritePostLogic.Favourite(_testPost.PostId, _testUser.UserId);

            Assert.That(FavouritePostLogic.IsCurrentUserFavourited(_testPost.PostId, _testUser.UserId) == true);

            FavouritePostLogic.Unfavourite(_testPost.PostId, _testUser.UserId);

        }

        [Test]
        public void Test_FavouritePostLogic_IsCurrentUserFavourited_NotFavourited()
        {
            Assert.That(FavouritePostLogic.IsCurrentUserFavourited(_testPost.PostId, _testUser.UserId) == false);
        }

        [Test]
        public void Test_FavouritePostLogic_GetFavouritePosts_NoFavouritedPosts()
        {
            Assert.That(FavouritePostLogic.GetFavouritePosts(_testPost.PostId, _testUser.UserId).Count == 0);
        }

        [Test]
        public void Test_PostCommentLogic_Comment()
        {
            int commentId = PostCommentLogic.Comment(_testUser.UserId, _testPost.PostId, "TestComment");
            string comment = _dBContext.PostComments.Where(postComment => postComment.PostCommentsId == commentId).First().CommentContend;
            _dBContext.PostComments.Remove(_dBContext.PostComments.Where(postComment => postComment.PostCommentsId == commentId).First());

            Assert.That(comment == "TestComment");
        }

        [Test]
        public void Test_PostCommentLogic_GetComments()
        {
            int commentId = PostCommentLogic.Comment(_testUser.UserId, _testPost.PostId, "TestComment");
            string comment = PostCommentLogic.GetComments(_testUser.UserId, _testPost.PostId).First();
            _dBContext.PostComments.Remove(_dBContext.PostComments.Where(postComment => postComment.PostCommentsId == commentId).First());

            Assert.That(comment == "TestComment");
        }

        [Test]
        public void Test_PostCommentLogic_RemoveComment()
        {
            int commentId = PostCommentLogic.Comment(_testUser.UserId, _testPost.PostId, "TestComment");
            PostCommentLogic.RemoveComment(commentId);

            Assert.Throws<InvalidOperationException>(
                () => {
                    string comment = _dBContext.PostComments.Where(postComment => postComment.PostCommentsId == commentId).First().CommentContend;
                });
        }

        [Test]
        public void Test_PostLikeLogic_Like_Unlike()
        {
            PostLikeLogic.Like(_testPost.PostId, _testUser.UserId);

            PostLike postLike = _dBContext.PostLikes.Where(postLike => postLike.PostId == _testPost.PostId && postLike.UserId == _testUser.UserId).First();
            PostLikeLogic.Unlike(_testPost.PostId, _testUser.UserId);

            Assert.IsNotNull(postLike);
        }

        [Test]
        public void Test_PostLikeLogic_IsCurrentUserLiked()
        {
            PostLikeLogic.Like(_testPost.PostId, _testUser.UserId);

            bool result = PostLikeLogic.IsCurrentUserLiked(_testPost.PostId, _testUser.UserId);
            PostLikeLogic.Unlike(_testPost.PostId, _testUser.UserId);

            Assert.IsNotNull(result);
        }

        [Test]
        public void Test_PostLikeLogic_LikeCount()
        {
            PostLikeLogic.Like(_testPost.PostId, _testUser.UserId);

            int result = PostLikeLogic.LikeCount(_testUser.UserId);
            PostLikeLogic.Unlike(_testPost.PostId, _testUser.UserId);

            Assert.IsNotNull(result == 1);
        }


    }
}
