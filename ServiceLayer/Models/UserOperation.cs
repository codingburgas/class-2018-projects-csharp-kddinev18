namespace ServiceLayer
{
    enum UserOperation
    {
        Register = 0,
        FinishRegistration = 1,
        LogIn = 2,
        LogInWithCookies = 3,
        GetPosts = 4,
        GetFavouritedPosts = 5,
        GetBlogPosts = 7,
        GetPostsByTag = 8,
        GetBlogs = 9,
        GetBlogsByName = 10,
        Like = 11,
        Unlike = 12,
        Favourite = 13,
        Unfavourite = 14,
        CheckIfBlogBelongsToUser = 15,
        Follow = 16,
        Unfollow = 17,
        GetFollowingBlogs = 18,
        Post = 19,
        GetTags = 20,
        GetAnalytics = 21,
        Comment = 22,
        GetComments = 23,
        CreateBlog = 24
    }
}
