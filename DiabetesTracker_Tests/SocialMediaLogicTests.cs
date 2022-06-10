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
    public class SocialMediaLogicTests
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

            Assert.That(result >= 1);
        }

        [Test]
        public void Test_PostLogic_AddPost()
        {
            Post post = PostLogic.AddPost(_testBlog.BlogId, null, "TestContent", new byte[] { 2, 4, 8, 16 }, _testUser.UserId);

            Assert.IsNotNull(post);
        }

        [Test]
        public void Test_PostLogic_GetPostBlogName()
        {
            string blogName = PostLogic.GetPostBlogName(_testPost.PostId);

            Assert.That(blogName == "TestBlogName");
        }

        [Test]
        public void Test_PostLogic_ArrangePosts()
        {
            List<PostInformation> posts = PostLogic.ArrangePosts(_testUser.UserId, 0);

            Assert.That(posts.Count >= 1);
        }

        [Test]
        public void Test_PostLogic_ArrangeFavouritePosts_NoFavouritePosts()
        {
            Assert.Throws<ArgumentNullException>(
                () => {
                    List<PostInformation> posts = PostLogic.ArrangeFavouritePosts(_testUser.UserId, 0);
                });
        }

        [Test]
        public void Test_PostLogic_ArrangeFavouritePosts_WithFavouritePosts()
        {
            FavouritePostLogic.Favourite(_testPost.PostId, _testUser.UserId);
            List<PostInformation> posts = PostLogic.ArrangeFavouritePosts(_testUser.UserId, 0);
            FavouritePostLogic.Unfavourite(_testPost.PostId, _testUser.UserId);

            Assert.That(posts.Count >= 1);
        }

        [Test]
        public void Test_PostLogic_GetPagedBlogPosts()
        {
            List<PostInformation> posts = PostLogic.ArrangeBlogPosts(_testUser.UserId, 0, _testBlog.BlogId);

            Assert.That(posts.Count >= 1);
        }

        [Test]
        public void Test_PostLogic_GetPostCount()
        {
            int postsCount = PostLogic.GetPostCount(_testUser.UserId);

            Assert.That(postsCount >= 1);
        }

        [Test]
        public void Test_TagLogic_AddTag()
        {
            Tag tag = TagLogic.AddTag("TestTagName");
            _dBContext.Tags.Remove(tag);

            Assert.IsNotNull(tag);
        }

        [Test]
        public void Test_TagLogic_ArraingeTags()
        {
            Tag tag = TagLogic.AddTag("TestTagName");
            List<TagInformation> tags = TagLogic.ArraingeTags();
            _dBContext.Tags.Remove(tag);

            Assert.IsNotNull(tags);
        }

        [Test]
        public void Test_PostTagLogic_AddPostTag()
        {
            Tag tag = TagLogic.AddTag("TestTagName");
            PostTag postTag = PostTagLogic.AddPostTag(_testPost, tag);
            _dBContext.Tags.Remove(tag);
            _dBContext.PostTags.Remove(postTag);

            Assert.IsNotNull(postTag);
        }

        public void Test_PostTagLogic_AddPostTag_WithExistingTag()
        {
            Tag tag = TagLogic.AddTag("TestTagName");
            PostTagLogic.AddPostTag(_testPost, tag);
            PostTag postTag = PostTagLogic.AddPostTag(_testPost, tag);
            _dBContext.Tags.Remove(tag);
            _dBContext.PostTags.Remove(postTag);

            Assert.IsNull(postTag);
        }
    }
}
