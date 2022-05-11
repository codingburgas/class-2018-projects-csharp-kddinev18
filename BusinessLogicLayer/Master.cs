using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class Master
    {
        private DiabetesTrackerDbContext _dbContext;
        public void OpenConnection()
        {
            _dbContext = new DiabetesTrackerDbContext();
            BlogBusinessLogic.DbContext = _dbContext;
            FavouritePostBusinessLogic.DbContext = _dbContext;
            FollowingBlogBusinessLogic.DbContext = _dbContext;
            MenuBusinessLogic.DbContext = _dbContext;
            MenuRecipeBusinessLogic.DbContext = _dbContext;
            PostBusinessLogic.DbContext = _dbContext;
            PostCommentBusinessLogic.DbContext = _dbContext;
            PostLikeBusinessLogic.DbContext = _dbContext;
            PostTagBusinessLogic.DbContext = _dbContext;
            RecipeBusinessLogic.DbContext = _dbContext;
            TagBusinessLogic.DbContext = _dbContext;
            UserBusinessLogic.DbContext = _dbContext;
            UserProfileBusinessLogic.DbContext = _dbContext;
        }

        public void CloseConnection()
        {
            _dbContext = null;
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
