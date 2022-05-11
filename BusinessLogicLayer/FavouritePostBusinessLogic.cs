using DataAccessLayer;
using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace BusinessLogicLayer
{
    public static class FavouritePostBusinessLogic
    {
        public static DiabetesTrackerDbContext DbContext { get; set; }
        public static void Favourite(Post post)
        {
            DbContext.FavouritePosts.Add(new FavouritePost()
            {
                UserId = User.GetCurrentUser(),
                PostId = post.PostId,
                SavedOn = DateTime.Now,
            });
        }
        public static ICollection<Post> GetFavouritePosts()
        {
            return DbContext.FavouritePosts.Where(favouritePost => favouritePost.UserId == UserBusinessLogic.GetCurrentUserId()).Include(favouritePost => favouritePost.Post).Select(favouritePost => favouritePost.Post).ToList();
        }
    }
}
