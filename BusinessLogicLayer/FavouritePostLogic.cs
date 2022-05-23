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
        public static void FavouritePost(int postId, int userId)
        {
            DbContext.FavouritePosts.Add(new FavouritePost()
            {
                UserId = userId,
                PostId = postId,
                SavedOn = DateTime.Now,
            });

            DbContext.SaveChanges();
        }
        public static void UnfavouritePost(int postId, int userId)
        {
            DbContext.FavouritePosts.Remove(DbContext.FavouritePosts.Where(favouritePost => favouritePost.UserId==userId && favouritePost.PostId == postId).First());
            DbContext.SaveChanges();
        }
        public static bool IsCurrentUserFavourited(int postId, int userId)
        {
            return DbContext.FavouritePosts.Where(favouritePost => favouritePost.PostId == postId && favouritePost.UserId == userId).FirstOrDefault() != null;
        }
        public static ICollection<Post> GetFavouritePosts(int userId, int skipCount)
        {
            return DbContext.FavouritePosts.Where(favouritePost => favouritePost.UserId == userId).Include(favouritePost => favouritePost.Post).Select(favouritePost => favouritePost.Post).Skip(skipCount).Take(10).ToList();
        }
    }
}
