using DataAccessLayer;
using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace BusinessLogicLayer
{
    public static class FavouritePostLogic
    {
        public static DiabetesTrackerDbContext DbContext { get; set; }

        // Favourites a post
        public static void Favourite(int postId, int userId)
        {
            // Add a new instance of a FavouritePost into the current context
            DbContext.FavouritePosts.Add(new FavouritePost()
            {
                UserId = userId,
                PostId = postId
            });

            // Save all changes made in this context into the database
            DbContext.SaveChanges();
        }

        // Unfavourites a post
        public static void Unfavourite(int postId, int userId)
        {
            DbContext.FavouritePosts
                // Remove the specified FavouritePost
                .Remove
                (
                    DbContext.FavouritePosts
                    // Where the user and the post that are favourite match the given post and user
                    .Where(favouritePost => favouritePost.UserId==userId && favouritePost.PostId == postId)
                    // Get the first result from the result set
                    .First()
                );

            // Save all changes made in this context into the database
            DbContext.SaveChanges();
        }

        // Checks weather a given user has favourited a post
        public static bool IsCurrentUserFavourited(int postId, int userId)
        {
            return DbContext.FavouritePosts
                // Where the user and the post that are favourite match the given post and user
                .Where(favouritePost => favouritePost.PostId == postId && favouritePost.UserId == userId)
                // Get the first result from the result set and checks if it is diffrent from null
                .FirstOrDefault() != null;
        }

        // Returns a list of posts favourited by the user
        public static List<Post> GetFavouritePosts(int userId, int skipCount)
        {
            return DbContext.FavouritePosts
                // Where the post is favourited by the user
                .Where(favouritePost => favouritePost.UserId == userId)
                // Include the navigation property (Post) from the Post table that is favourited by the given user
                .Include(favouritePost => favouritePost.Post)
                // Select only the property Posts from the whole result set
                .Select(favouritePost => favouritePost.Post)
                // Skip some results based on the given skip count
                .Skip(skipCount)
                // Take only 10 rows from the result set
                .Take(10)
                // Convert the result set to a list
                .ToList();
        }
    }
}
