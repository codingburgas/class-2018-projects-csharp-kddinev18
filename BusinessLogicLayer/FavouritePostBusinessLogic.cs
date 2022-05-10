using DataAccessLayer;
using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace BusinessLogicLayer
{
    public class FavouritePostBusinessLogic
    {
        public static void Favourite(DiabetesTrackerDbContext dbContext, Post post)
        {
            dbContext.FavouritePosts.Add(new FavouritePost()
            {
                UserId = User.GetCurrentUser(),
                PostId = post.PostId,
                SavedOn = DateTime.Now,
            });
        }
        public static ICollection<Post> GetFavouritePosts(DiabetesTrackerDbContext dbContext)
        {
            return dbContext.FavouritePosts.Where(favouritePost => favouritePost.UserId == UserBusinessLogic.GetCurrentUser()).Include(favouritePost => favouritePost.Post).Select(favouritePost => favouritePost.Post).ToList();
        }
    }
}
