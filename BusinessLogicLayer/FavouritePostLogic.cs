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
        public static void Favourite(int postId, int userId)
        {
            DbContext.FavouritePosts.Add(new FavouritePost()
            {
                UserId = userId,
                PostId = postId,
                SavedOn = DateTime.Now,
            });
        }
        public static ICollection<Post> GetFavouritePosts(int userId)
        {
            return DbContext.FavouritePosts.Where(favouritePost => favouritePost.UserId == userId).Include(favouritePost => favouritePost.Post).Select(favouritePost => favouritePost.Post).ToList();
        }

        public static bool IsCurrentUserFavourited(int postId, int userId)
        {
            return DbContext.FavouritePosts.Where(favouritePost => favouritePost.PostId == postId && favouritePost.UserId == userId).FirstOrDefault() != null;
        }
    }
}
