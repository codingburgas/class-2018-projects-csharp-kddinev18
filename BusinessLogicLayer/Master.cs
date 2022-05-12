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
        public static void OpenConnection()
        {
            DiabetesTrackerDbContext dbContext = new DiabetesTrackerDbContext();
            BlogBusinessLogic.DbContext = dbContext;
            FavouritePostBusinessLogic.DbContext = dbContext;
            FollowingBlogBusinessLogic.DbContext = dbContext;
            MenuBusinessLogic.DbContext = dbContext;
            MenuRecipeBusinessLogic.DbContext = dbContext;
            PostBusinessLogic.DbContext = dbContext;
            PostCommentBusinessLogic.DbContext = dbContext;
            PostLikeBusinessLogic.DbContext = dbContext;
            PostTagBusinessLogic.DbContext = dbContext;
            RecipeBusinessLogic.DbContext = dbContext;
            TagBusinessLogic.DbContext = dbContext;
            UserBusinessLogic.DbContext = dbContext;
            UserProfileBusinessLogic.DbContext = dbContext;
        }

        public static void CloseConnection()
        {
            BlogBusinessLogic.DbContext = null;
            FavouritePostBusinessLogic.DbContext = null;
            FollowingBlogBusinessLogic.DbContext = null;
            MenuBusinessLogic.DbContext = null;
            MenuRecipeBusinessLogic.DbContext = null;
            PostBusinessLogic.DbContext = null;
            PostCommentBusinessLogic.DbContext = null;
            PostLikeBusinessLogic.DbContext = null;
            PostTagBusinessLogic.DbContext = null;
            RecipeBusinessLogic.DbContext = null;
            TagBusinessLogic.DbContext = null;
            UserBusinessLogic.DbContext = null;
            UserProfileBusinessLogic.DbContext = null;
        }
    }
}
