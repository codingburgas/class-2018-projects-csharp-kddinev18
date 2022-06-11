using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public static class Master
    {
        public static DiabetesTrackerDbContext OpenConnection()
        {
            DiabetesTrackerDbContext dbContext = new DiabetesTrackerDbContext();
            BlogLogic.DbContext = dbContext;
            FavouritePostLogic.DbContext = dbContext;
            FollowingBlogLogic.DbContext = dbContext;
            MenuLogic.DbContext = dbContext;
            MenuRecipeLogic.DbContext = dbContext;
            PostLogic.DbContext = dbContext;
            PostCommentLogic.DbContext = dbContext;
            PostLikeLogic.DbContext = dbContext;
            PostTagLogic.DbContext = dbContext;
            RecipeLogic.DbContext = dbContext;
            TagLogic.DbContext = dbContext;
            UserLogic.DbContext = dbContext;
            UserProfileLogic.DbContext = dbContext;
            return dbContext;
        }

        public static void CloseConnection()
        {
            BlogLogic.DbContext = null;
            FavouritePostLogic.DbContext = null;
            FollowingBlogLogic.DbContext = null;
            MenuLogic.DbContext = null;
            MenuRecipeLogic.DbContext = null;
            PostLogic.DbContext = null;
            PostCommentLogic.DbContext = null;
            PostLikeLogic.DbContext = null;
            PostTagLogic.DbContext = null;
            RecipeLogic.DbContext = null;
            TagLogic.DbContext = null;
            UserLogic.DbContext = null;
            UserProfileLogic.DbContext = null;
        }
    }
}
